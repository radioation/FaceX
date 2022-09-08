
import socket
import sys

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
    receiveBytes = sock.recvfrom(4096)

    print('Read %s bytes' % (len(receiveBytes) ))
    fileTime = int.from_bytes( receiveBytes, "little" );
    offset = 8
    name = print( receiveBytes[offset:offset+32])
    offset +=32
    ObjectIntersectionName =  print( receiveBytes[offset:offset+64])
    offset +=64
    IntersectionIndex = int.from_bytes( receiveBytes[offset:offset+4], "little" )
    offset += 4
    ObjectIntersectionX = float.from_bytes( receiveBytes[offset:offset+4], "little" )
    offset += 4
    ObjectIntersectionY = float.from_bytes( receiveBytes[offset:offset+4], "little" )
    offset += 4


     