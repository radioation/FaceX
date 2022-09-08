
import socket

GroupAddress = '234.5.6.7'
EndPoint( "", 44511 ) 

#Creates a UdpClient for reading incoming data.
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
  
# join 234.5.6.7
print('Attempting to join multicast group\n')
sock.bind(EndPoint)
group = socket.inet_aton(GroupAddress)
mreq = struct.pack('4sL', group, socket.INADDR_ANY)
sock.setsockopt(socket.IPPROTO_IP, socket.IP_ADD_MEMBERSHIP, mreq)


while True: 
    data = sock.recvfrom(4096)

    print('Read %s bytes' % (len(data) ))
    print(data)
     