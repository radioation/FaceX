import json
import socket
import sys
import struct

# Define the UDP multicast address and endpoint
GroupAddress = '234.5.6.7'
EndPoint = ( "", 44515 )
InterfaceAddress = '192.168.1.2'
#InterfaceAddress = ''  


# Create a UDP socket and join the multicast group
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)


# join 234.5.6.7
print('Attempting to join multicast group\n')
sock.bind(EndPoint)
group = socket.inet_aton(GroupAddress)
if InterfaceAddress == '':
  mreq = struct.pack('4sL', group, socket.INADDR_ANY)
else:
  mreq = socket.inet_aton(GroupAddress) + socket.inet_aton(InterfaceAddress)
sock.setsockopt(socket.IPPROTO_IP, socket.IP_ADD_MEMBERSHIP, mreq)


print("start listening")
# Receive the message and decode the JSON payload
while True:
  receiveBytes = sock.recv(4096)

  print(receiveBytes)
  data = json.loads(receiveBytes.decode('utf-8'))
  # Print the JSON payload
  print("--RAW Data----------------------------")
  print(data)
  print("--Parsed Data-------------------------")
  subjectName = data["info"]["subject"]
  timestamp = data["timestamp"]
  combinedValid = data["combined"]["validity"]
  if combinedValid:
    closestIntersection = data["combined"]["closest"]
    print("Subject: ", subjectName )
    print("Timestamp: ", timestamp )
    print("Closest Intersection: ", closestIntersection )

  

