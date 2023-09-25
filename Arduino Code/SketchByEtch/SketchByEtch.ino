//Requires the Responsive analogread library. this can easily be found in the library manager. might end up removing this and handle value smoothing manually or in pc app
#include <ResponsiveAnalogRead.h>

char value[1]; 
char serialDelay[2];
int delayLength = 0;
bool curPress;
//senddata is used to loop sending serialdata with intervals equal to the delayLength.
//delays will be done using the millis command as this will allow the arduino to still receive serial data to stop the dataflood.
bool sendData = false;
ResponsiveAnalogRead analog1(A0, true);
ResponsiveAnalogRead analog2(A1, true);
String sender;
String part;

bool serialLoop = false;
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

void loop() 
{

  if(Serial.available())
  {
    Serial.readBytes(value, 1);
    switch (value[0])
    {
    case 'S':
      serialLoop = true;
      if(Serial.readBytes(value, 1))
      {
        if(value[1] == 'N')
        {
          delayLength = 0;
          Serial.flush();
          break;
        }
        if(value[1] == 'D')
        {
          int delayMs;
          Serial.readBytes(serialDelay, 2);
          //left to right, like a normal number
          delayMs += serialDelay[0];
          delayMs <<= 8;
          delayMs += serialDelay[1];
          delayLength = delayMs;
        }
      }
      Serial.flush();
      break;

    case '-':
      Serial.println('?');
      Serial.flush();
      break;

    default:
      Serial.flush();
      break;
    }
  }

  while(serialLoop)
  {
    analog1.update();
    analog2.update();
    sender = String((int)(analog1.getValue()), DEC);
    sender += ",";
    part = String((int)(analog2.getValue()), DEC);
    sender += part;
    sender += "|";
    sender += String((digitalRead(3)), DEC);

    CheckLoop();
    Serial.println(sender);
    Serial.flush();
  }
} 

void CheckLoop()
{
  if(Serial.available())
  {
    Serial.readBytes(value, 1);
    if(value[0] == 'E') serialLoop = false;
  }
}

/*
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
*/
