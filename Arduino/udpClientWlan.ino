#include <SPI.h>
#include <WiFiNINA.h>

const int ledPin = 13;     // Pin 13 for the LED
const int udpPort = 8888;  // UDP port
char ssid[] = "mr_router_labor";  // your network SSID (name)
char pass[] = "wearedoingresearch.";  // your network password
char hostName[] = "arduinoNanoIoT";
int status = WL_IDLE_STATUS;  // the WiFi radio's status
char packetBuffer[255]; // Buffer for storing incoming packets
int motorPin = 2; // The pin to which the motor is connected
int motorPin2 = 3; // The pin to which the motor is connected

WiFiUDP Udp;                  // UDP object for communication

void setup() {
  // Initialize serial and wait for the port to open:
  Serial.begin(9600);
  pinMode(ledPin, OUTPUT);  // Set LED pin as an output
  connectToWiFi();
  printCurrentNet();
  printWifiData();
  // Start UDP server
  Udp.begin(udpPort);
  // Set the motor pin as an output
  pinMode(motorPin, OUTPUT);
  pinMode(motorPin2, OUTPUT);
}

void loop() {
  vibrationMotors();
}

void vibrationMotors() {
  // receives message and devides them in pieces
  int packetSize = Udp.parsePacket();
  if (packetSize) {
    Udp.read(packetBuffer, 255);
    char *ptr = strtok(packetBuffer, ",");
    // Vibration part
    if (ptr != NULL && strcmp(ptr, "start vibration") == 0) {
      ptr = strtok(NULL, ",");
      if (ptr != NULL) {
        int strength1 = atoi(ptr);  // Convert the string to an integer
        ptr = strtok(NULL, ",");
        if (ptr != NULL) {
          int strength2 = atoi(ptr);
          
          // Check if the strengths are below 130 and above 0
          // If this is the case, the motors need starting assistance. 
          // Vibrates 1 second for each message   
          if ((strength1 > 0 && strength1 < 130) || (strength2 > 0 && strength2 < 130)) {
            analogWrite(motorPin, 255);
            analogWrite(motorPin2, 255);
            delay(30); // 
            analogWrite(motorPin, strength1);
            analogWrite(motorPin2, strength2);
            delay(970);
            analogWrite(motorPin, 0);
            analogWrite(motorPin2, 0);
            
          } else {
            analogWrite(motorPin, strength1);
            analogWrite(motorPin2, strength2);
            delay(1000);
            analogWrite(motorPin, 0);
            analogWrite(motorPin2, 0);
          }
        }
      }
    } else {
      // Kalibration part
      if (ptr != NULL && strcmp(ptr, "start kalibration") == 0) {
      ptr = strtok(NULL, ",");
      if (ptr != NULL) {
        int strength1 = atoi(ptr);  // Convert the string to an integer
        ptr = strtok(NULL, ",");
        if (ptr != NULL) {
          int strength2 = atoi(ptr);
          
          // Check if the strengths are below 130 and above 0
          // If this is the case, the motors need starting assistance. 
          // Vibrates 2 second for each message   
          if ((strength1 > 0 && strength1 < 130) || (strength2 > 0 && strength2 < 130)) {
            analogWrite(motorPin, 255);
            analogWrite(motorPin2, 255);
            delay(30); // Added delay to run the motors briefly
            analogWrite(motorPin, strength1);
            analogWrite(motorPin2, strength2);
            delay(1970);
            analogWrite(motorPin, 0);
            analogWrite(motorPin2, 0);
            
          } else {
            analogWrite(motorPin, strength1);
            analogWrite(motorPin2, strength2);
            delay(2000);
            analogWrite(motorPin, 0);
            analogWrite(motorPin2, 0);
          }
        }
      }
    }
      

    }
  }
}


void swapLED() {

  // Wait for incoming UDP packets
  int packetSize = Udp.parsePacket();
  if (packetSize) {
    char packetData = Udp.read();
    if (packetData == '1') {
      digitalWrite(ledPin, HIGH);  // Turn LED on
      Udp.beginPacket(Udp.remoteIP(), Udp.remotePort());
      Udp.write("LED turned on");
      Udp.endPacket();
    } else if (packetData == '0') {
      digitalWrite(ledPin, LOW);  // Turn LED off
      Udp.beginPacket(Udp.remoteIP(), Udp.remotePort());
      Udp.write("LED turned off");
      Udp.endPacket();
    }
  }
}

void connectToWiFi() {
  // Check if the WiFi module is working correctly
  if (WiFi.status() == WL_NO_MODULE) {
    Serial.println("Communication with WiFi module failed!");
    // don't continue
    while (true);
  }

  WiFi.setHostname(hostName);  // Set the hostname

  // Attempt to connect to WiFi network
  while (status != WL_CONNECTED) {
    Serial.print("Attempting to connect to SSID: ");
    Serial.println(ssid);
    // Connect to WPA/WPA2 network. Change this line if using an open or WEP network:
    status = WiFi.begin(ssid, pass);
    // Blinking if Arduino is trying to connect to wifi
    digitalWrite(LED_BUILTIN, HIGH);  // LED on
    delay(500);
    digitalWrite(LED_BUILTIN, LOW);  // LED off
    delay(500);
  }
}

void printWifiData() {
  // print your board's IP address:
  IPAddress ip = WiFi.localIP();
  Serial.print("IP Address: ");
  Serial.println(ip);
  Serial.println(String("Host name: ") + hostName);

  // print your MAC address:
  byte mac[6];
  WiFi.macAddress(mac);
  Serial.print("MAC address: ");
  printMacAddress(mac);
}

void printCurrentNet() {
  // if connection is successful the LED turns on
  digitalWrite(LED_BUILTIN, HIGH);
  // print the SSID of the network you're attached to:
  Serial.print("SSID: ");
  Serial.println(WiFi.SSID());

  // print the MAC address of the router you're attached to:
  byte bssid[6];
  WiFi.BSSID(bssid);
  Serial.print("BSSID: ");
  printMacAddress(bssid);

  // print the received signal strength:
  long rssi = WiFi.RSSI();
  Serial.print("signal strength (RSSI):");
  Serial.println(rssi);

  // print the encryption type:
  byte encryption = WiFi.encryptionType();
  Serial.print("Encryption Type:");
  Serial.println(encryption, HEX);
  Serial.println();
}

void printMacAddress(byte mac[]) {
  for (int i = 5; i >= 0; i--) {
    if (mac[i] < 16) {
      Serial.print("0");
    }
    Serial.print(mac[i], HEX);
    if (i > 0) {
      Serial.print(":");
    }
  }
  Serial.println();
}
