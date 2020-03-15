#include <ResponsiveAnalogRead.h>

byte value[1];
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
  Serial.begin(9600);
}

void loop() {

  while(Serial.available() > 0)
  {
      Serial.readBytes(value, 1);
  if( value[0] == '.')
  {
    analog1.update();
    analog2.update();
    sender = String(analog1.getValue(), DEC);
    sender += ",";
    part = String(analog2.getValue(), DEC);
    sender += part;
    sender += "|";
    curPress = digitalRead(3);
    sender += String((curPress && prevPress1 != curPress), DEC);
    prevPress1 = curPress;
    sender += "|";
    curPress = digitalRead(4);
    sender += String((curPress && prevPress2 != curPress), DEC);
    prevPress2 = curPress;
    Serial.println(sender);
    Serial.flush();
  }
  if(value[0] == '-')
  {
    Serial.println('?');
  }
  }

}
