//Requires the Responsive analogread library. this can easily be found in the library manager
#include <ResponsiveAnalogRead.h>

char value[1];
int prevVal = 0;
int val;
bool prevPress1 = LOW;
bool prevPress2 = LOW;
bool curPress;
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
  //You can remove the analogReadResolution line if you want or change the number to something else.
  //It is simply here to ecord any analog values in a higher resolution so cursor positioning can become more accurate.
  analogReadResolution(12);
}

void loop() {

  while(Serial.available() > 0)
  {
      Serial.readBytes(value, 1);
  if( value[0] == '.')
  {
    analog1.update();
    analog2.update();
    sender = String((int)(analog1.getValue()), DEC);
    sender += ",";
    part = String((int)(analog2.getValue()), DEC);
    sender += part;
    sender += "|";
    curPress = digitalRead(3);
    sender += String((curPress && prevPress1 != curPress), DEC);

    Serial.println(sender);
    Serial.flush();
  }
  if(value[0] == '-')
  {
    Serial.println('?');
  }
  }

}
