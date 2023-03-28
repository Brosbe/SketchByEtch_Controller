//Requires the Responsive analogread library. this can easily be found in the library manager
#include <ResponsiveAnalogRead.h>

char value[1];
char serialDelay[2];
int delayLength = 0;
int newDelayLength = 0;
bool curPress;
//senddata is used to loop sending serialdata with intervals equal to the delayLength.
//delays will be done using the millis command as this will allow the arduino to still receive serial data to stop the dataflood.
bool sendData = false;
ResponsiveAnalogRead analog1(A0, true);
ResponsiveAnalogRead analog2(A1, true);
String sender;
String part;
void setup() {
  // put your setup code here, to run once:
  pinMode(A0, INPUT);
  pinMode(A1, INPUT);
  pinMode(3, INPUT);
  Serial.begin(19200);
  analogReadResolution(12);
  analog1.setAnalogResolution(4096);
  analog2.setAnalogResolution(4096);
  //You can remove the analogReadResolution line if you want or change the number to something else.
  //It is simply here to ecord any analog values in a higher resolution so cursor positioning can become more accurate.
}

void loop() {

  while(Serial.available())
  {
    Serial.readBytes(value, 1);
    switch (value[0])
    {
    case '.':
      analog1.update();
      analog2.update();
      sender = String((int)(analog1.getValue()), DEC);
      sender += ",";
      part = String((int)(analog2.getValue()), DEC);
      sender += part;
      sender += "|";
      sender += String((digitalRead(3)), DEC);

      Serial.println(sender);
      Serial.flush();
      break;

    case '-':
      Serial.println('?');
      Serial.flush();
      break;

    case 'd':
      newDelayLength = 0;
      Serial.readBytes(serialDelay, 2);
      for (byte i = 0; i < 2; i++)
      {
        newDelayLength <<= 8;
        newDelayLength += serialDelay[i];
      }
      delayLength = newDelayLength;

      break;

    default:
      Serial.flush();
      break;
    }
  }
} 
