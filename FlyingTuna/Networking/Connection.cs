using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using FlyingTuna.MPI;
using FlyingTuna.Networking.Packets;
using FlyingTuna.Networking.Processors;

namespace FlyingTuna.Networking
{
    public class Connection : IMessageSender
    {
        private readonly IPacketProcessor[] _packetProcessors;

        private readonly byte[] _readBuffer;
        private readonly MemoryStream _readStream;
        private readonly byte[] _writeBuffer;
        private readonly MemoryStream _writeStream;

        private readonly Socket _netStream;
        private readonly BinaryWriter _writer;
        private readonly BinaryReader _reader;
        public readonly int BufferSize;

        public bool Connected
        {
            get { return _netStream.Connected; }
        }

        public Connection(Socket stream, IPacketProcessor[] processors, int bufferSize = 1024 * 6)
        {
            _netStream = stream;
            BufferSize = bufferSize;
            _packetProcessors = processors;

            _readBuffer = new byte[bufferSize];
            _readStream = new MemoryStream(_readBuffer);
            _reader = new BinaryReader(_readStream);

            _writeBuffer = new byte[bufferSize];
            _writeStream = new MemoryStream(_writeBuffer);
            _writer = new BinaryWriter(_writeStream);
        }

        private static bool IsConnected(Socket socket)
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException) { return false; }
        }

        public void RecieveData()
        {
            if (!IsConnected(_netStream))
            {
                Console.WriteLine("Lost connection");
                _netStream.Disconnect(false);
            }

            if(_netStream.Available > 0)
            {
                var total = _netStream.Receive(_readBuffer, 0, _netStream.Available, SocketFlags.None);

                Console.WriteLine("Reading {0} bytes", total);

                // Process the stream
                while (_readStream.Position < total)
                {
                    int packet = _reader.ReadByte();

                    if (packet != -1)
                    {
                        short len = _reader.ReadInt16();

                        Console.WriteLine("{2} --> #: {0} Length: {1}", packet, len, _netStream.RemoteEndPoint);
                        for (int i = (int)_readStream.Position; i < _readStream.Position + len; i++)
                        {
                            Console.Write("{0:X2} ", _readBuffer[i]);
                        }
                        Console.WriteLine();
                        
                        Console.WriteLine("Packet will be processed by: " + _packetProcessors[packet]);

                        _packetProcessors[packet].Process(this, _readBuffer, (short)_readStream.Position, len);

                        _readStream.Position += len;
                    }
                }

                // Reset stream position and start again
                _readStream.Position = 0;
            }
        }

        public int DataToSend { get; private set; }

        public void WriteData(IPacket packet)
        {
            var ms = new MemoryStream();

            var w = new BinaryWriter(ms);
            var len = packet.WriteToStream(w);

            var buf = ms.GetBuffer();

            Console.WriteLine("{2} <-- #: {0} Length: {1}", packet.Number, len, _netStream.RemoteEndPoint);
            for (int i = 0; i < len; i++)
            {
                Console.Write("{0:X2} ", buf[i]);
            }
            Console.WriteLine();

            _writer.Write(packet.Number);
            _writer.Write(len);
            _writer.Write(buf, 0, len);

            DataToSend += len + 3;
        }

        public void Flush()
        {
            _writer.Flush();

            if(DataToSend == 0)
            {
                return;
            }

            _netStream.Send(_writeBuffer, 0, DataToSend, SocketFlags.None);

            DataToSend = 0;
            _writeStream.Position = 0;
        }

        private readonly Dictionary<string, object> _metadata = new Dictionary<string, object>();
        //TODO: Properly create the metadata interface
        public Dictionary<string, object> GetMetadata()
        {
            return _metadata;
        }

        public void Error(Message message, ErrorMessage errorMessage)
        {
            throw new NotImplementedException();
        }

        public void Disconnect(string genericGamefull)
        {
            _netStream.Disconnect(false);
        }

        public void SendMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
