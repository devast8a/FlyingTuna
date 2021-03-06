using System;
using System.Collections.Generic;
using System.Diagnostics;
using FlyingTuna.Additions;
using FlyingTuna.Additions.IdenSys;
using FlyingTuna.Additions.Metadata;
using FlyingTuna.Additions.VarRefs;
using FlyingTuna.Components;
using FlyingTuna.MPI;
using FlyingTuna.Networking;
using FlyingTuna.Networking.Packets;
using Microsoft.Xna.Framework;

namespace FlyingTuna.Entities
{
    public class Entity : MetadataContainer, IVariableContainer, IMessageListener
    {
        private readonly Dictionary<Type, ComponentListener> _listeners = new Dictionary<Type, ComponentListener>();
        private readonly Dictionary<Type, Component> _components = new Dictionary<Type, Component>();
        public readonly VariableContainer Container = new VariableContainer();

        private readonly ComponentFactory _componentFactory;

        public readonly IHost Host;
        public readonly ID Identifier;
        public readonly EntityType Type;
        public readonly IMessageSender LocalSender;

        public Entity(ComponentFactory componentFactory, IHost host, ID identifier, EntityType type)
        {
            _componentFactory = componentFactory;
            Host = host;
            Identifier=identifier;
            Type=type;

            LocalSender = host.ServiceManager.GetProvider<IMessageSender>();
        }

        public Component Add(Type type)
        {
            if(!typeof(Component).IsAssignableFrom(type))
            {
                throw new Exception();
            }

            return AddImpl(type);
        }

        public T Add<T>() where T : Component
        {
            return (T)AddImpl(typeof(T));
        }

        public Component AddImpl(Type type)
        {
            var component = _componentFactory.GetComponent(type);
            _components.Add(component.GetType(), component);

            foreach (var listenerDecl in component.Type.Listeners)
            {
                ComponentListener listener;

                if(!_listeners.TryGetValue(listenerDecl.Key, out listener))
                {
                    listener = new ComponentListener();
                    _listeners.Add(listenerDecl.Key, listener);
                }

                listener.Add(component, listenerDecl.Value);
            }

            foreach (var dep in component.Type.Dependancies)
            {
                Component value;

                if (!_components.TryGetValue(dep.Key, out value))
                {
                    value = Add(dep.Key);
                }

                dep.Value.Set(component, value);
            }

            foreach (var svc in component.Type.Services)
            {
                svc.Value.Set(component, Host.ServiceManager.GetProvider(svc.Value.Type));
            }

            foreach (var variable in component.Type.Variables)
            {
                variable.Value.Set(component, Container.GetOrCreate(variable.Key, variable.Value.Type));
            }

            component.Entity = this;
            component.OnInitialize(Host);

            return component;
        }

        public T Get<T>() where T : Component
        {
            Component value;
            return _components.TryGetValue(typeof(T), out value) ? (T)value : null;
        }

        public void SendMessage(Message message)
        {
            SendMessageAs(LocalSender, message);
        }

        public void SendMessageAs(IMessageSender sender, Message message)
        {

            ComponentListener value;
            if (!_listeners.TryGetValue(message.GetType(), out value))
            {
                return;
            }

            value.Invoke(sender, message);
        }

        public void SendRemoteMessage(Message message)
        {
            if(_connections.Count == 0)
            {
                return;
            }

            var packet = new EntityMessage(Host, Identifier.IdentifierNumber, message);

            foreach(var con in _connections)
            {
                con.WriteData(packet);
            }
        }

        /// <summary>
        /// Send a message to an entity through a specified connection.
        /// </summary>
        /// <param name="message">The message to send</param>
        /// <param name="connection">Connection to send the message to</param>
        public void SendMessage(Message message, Connection connection)
        {
            var packet = new EntityMessage(Host, Identifier.IdentifierNumber, message);

            foreach (var con in _connections)
            {
                con.WriteData(packet);
            }
        }

        /// <summary>
        /// Sends a message locally and remotely at the same time
        /// </summary>
        /// <param name="message">The message to send</param>
        public void BroadcastMessage(Message message)
        {
            SendMessage(message);
            SendRemoteMessage(message);
        }

        readonly List<Connection> _connections = new List<Connection>();

        public void SendTo(Connection connection)
        {
            _connections.Add(connection);
            connection.WriteData(new EntityNew(Type, Identifier.IdentifierNumber, EntityNewFlags.None));
        }

        public void NewConnection(Connection connection)
        {
            _connections.Add(connection);
        }

        public void Overwrite(VariableReference varRef)
        {
            Container.Overwrite(varRef);

            foreach(var component in _components)
            {
                component.Value.OverwriteLocal(varRef);
            }
        }

        public IEnumerable<VariableReference> GetVariables()
        {
            return Container.GetVariables();
        }

        public void Set<T>(string name, T value)
        {
            Container.Set(name, value, this);
        }
    }
}