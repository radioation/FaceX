
import socket
import sys
import struct

GroupAddress = '234.5.6.7'
EndPoint = ( "", 44511 ) 

#Creates a UdpClient for reading incoming data.
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
  
# join 234.5.6.7
print('Attempting to join multicast group\n')
sock.bind(EndPoint)
group = socket.inet_aton(GroupAddress)
mreq = struct.pack('4sL', group, socket.INADDR_ANY)
sock.setsockopt(socket.IPPROTO_IP, socket.IP_ADD_MEMBERSHIP, mreq)


while True: 
    receiveBytes = sock.recv(4096)

    fileTime = int.from_bytes( receiveBytes[0:8], "little" )
    offset = 8
    name = receiveBytes[offset:offset+32].decode('utf-8')
    offset +=32
    ObjectIntersectionName =  receiveBytes[offset:offset+64].decode('utf-8')
    offset +=64
    IntersectionIndex = int.from_bytes(receiveBytes[offset:offset + 4], "little")
    offset += 4
    ObjectIntersectionX, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4
    ObjectIntersectionY, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4

    
    HeadPosX, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4
    HeadPosY, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4
    HeadPosZ, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4


    
    LeftGazeOriginX, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4
    LeftGazeOriginY, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4
    LeftGazeOriginZ, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4

        
    LeftGazeDirectionX, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4
    LeftGazeDirectionY, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4
    LeftGazeDirectionZ, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4

        
    RightGazeOriginX, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4
    RightGazeOriginY, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4
    RightGazeOriginZ, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4

        
    RightGazeDirectionX, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4
    RightGazeDirectionY, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4
    RightGazeDirectionZ, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4

    
    LeftGazeType = int.from_bytes(receiveBytes[offset:offset + 4], "little")
    offset += 4
    RightGazeType = int.from_bytes(receiveBytes[offset:offset + 4], "little")
    offset += 4
        
    HeadX, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4
    HeadY, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4
    HeadZ, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4

        
    HeadDirectionX, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4
    HeadDirectionY, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4
    HeadDirectionZ, = struct.unpack('f',receiveBytes[offset:offset + 4])
    offset += 4
     

    print(fileTime, name, ObjectIntersectionName,IntersectionIndex,ObjectIntersectionX,ObjectIntersectionY,HeadPosX,HeadPosY,HeadPosZ,LeftGazeOriginX,LeftGazeOriginY,LeftGazeOriginZ,LeftGazeDirectionX,LeftGazeDirectionY,LeftGazeDirectionZ,RightGazeOriginX,RightGazeOriginY,RightGazeOriginZ,RightGazeDirectionX,RightGazeDirectionY,RightGazeDirectionZ,LeftGazeType,RightGazeType,HeadX,HeadY,HeadZ,HeadDirectionX,HeadDirectionY,HeadDirectionZ); 


     