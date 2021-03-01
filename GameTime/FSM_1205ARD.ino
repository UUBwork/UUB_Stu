#include <Rtc_Pcf8563.h>
#include <Wire.h>
#include <LiquidCrystal_I2C.h>

Rtc_Pcf8563 rtc;
LiquidCrystal_I2C lcd(0x27, 2, 1, 0, 4, 5, 6, 7, 3, POSITIVE);
const int bluePin = 3;
const int greenPin = 5;
const int redPin = 6;
int micA = A0; //設定麥克風的腳位
int micV;  //存麥克風所產生的音量
const int buz = 13; //蜂鳴器腳位

int state = 0;
int butSetv = 0;
int butUpv = 0;
int butDownv = 0;
int butOKv = 0;

const int butSet = 2;
const int butUp = 7;
const int butDown = 8;
const int butOK = 12 ;
int x = 0;
int y = 0;
int butS = 0;
int butS3 = 0;
int butS4 = 0;
int butS5 = 0;
int buttle0 = 0;
int buttle1 = 0;
int buttle2 = 0;
int buttle3 = 0;
int buttle4 = 0;
int gmaeint1 = 0;
int gmaeint2 = 0;
int gmaeint3 = 0;
int Mandatory = 0;
int gmaemun1 = 0;
int newtine = 0;
int hms = 0;
int timesetv[3] = {0, 0, 0};
byte h = 0;
byte m = 0;
byte s = 0;
int tbut=0;
int tbutt=0;

int cat=0;

boolean ledoc = false;




void setup() {
 // rtc.setTime(17,16,00);
  //cat=1718;
  // put your setup code here, to run once:
  state = 0;
  rtc.setDate(5,1 , 12, 0, 17);
  MainState(state);
  pinMode(buz, OUTPUT);
  pinMode(butSet, INPUT);
  pinMode(butUp, INPUT);
  pinMode(butDown, INPUT);
  pinMode(butOK, INPUT);
  lcd.begin(20, 4);
  lcd.setCursor(0, 0);
  lcd.print("123");
  lcd.cursor();
  lcd.blink();
  pinMode(buz, OUTPUT); //蜂鳴器設定輸出
}

void loop() {
  // put your main code here, to run repeatedly:
  int butSetv = digitalRead(butSet);
  
  if(rtc.getHour()*100+rtc.getMinute()==cat){
   state=5;
    
  }
  
  if (butSetv == HIGH && state < 2) {
    state++;
    lcd.clear();
    delay(500);
    //MainState(state);
    buttle0 = 0;
    buttle1 = 0;
    buttle2 = 0;
    buttle3 = 0;
    buttle4 = 0;
    gmaeint1 = 0;
  }
  else if (butSetv == HIGH && state == 2) {
    state = 0;
    lcd.clear();
    //TimeState();
    delay(500);
    buttle0 = 0;
    buttle1 = 0;
    buttle2 = 0;
    buttle3 = 0;
    buttle4 = 0;
    gmaeint1 = 0;

  }
  //digitalWrite(buz, HIGH);
  MainState(state);
  if (ledoc) {
    superled();
  }


}

void MainState(int a) {

  switch (a) {
    case 0:
      TimeState();
      break;
    case 1:
      SetState();
      break;
    case 2:
      GameState();
      break;
    case 5:
      AlarmState();
      break;
    case 8:
      Timechange();
      break;
    case 9:
      TimeSet();
      break;
    case 10:
      LED();
      break;
    case 100:
      RPS();
      break;
  }

}


void TimeState() {
  if (newtine == 0) {
    lcd.clear();
    newtine++;
  }
  lcd.setCursor(0, 0);
  lcd.print("Now Time");
  //String w=rtc.getDOWStr();//周幾
  String d = rtc.formatDate(); //日期
  String t = rtc.formatTime(); //時間
  //lcd.clear();
  //lcd.println(d);
  lcd.setCursor(0, 1);
  lcd.print(t); //讓lcd輸出時間
  lcd.setCursor(0, 2);
  lcd.print(d);
  lcd.setCursor(0, 3);
  lcd.print(cat);

  //superled();
}

void SetState() {
  if (buttle0 == 0) {
    lcd.clear();
    newtine = 0;
    lcd.setCursor(0, 0);
    lcd.print("Time change");
    lcd.setCursor(0, 1);
    lcd.print("Time Set");
    lcd.setCursor(0, 2);
    lcd.print("LED");
    lcd.setCursor(y + 19, x);
    buttle0++;
    butS = 0;
    x = 0;
    y = 0;
    buttle3 = 0;
    buttle2 = 0;
    buttle1 = 0;
  }

  int butDownv = digitalRead(butDown);
  int butUpv = digitalRead(butUp);
  int butOKv = digitalRead(butOK);
  if (butDownv == HIGH && butS < 2) {
    butS++;
    lcd.setCursor(19, x + butS);
    delay(500);
  }
  else if (butDownv == HIGH && butS >= 2) {
    butS = 0;
    lcd.setCursor(19, x + butS);
    delay(500);
  }
  else if (butUpv == HIGH && butS <= 0) {
    butS = 2;
    lcd.setCursor(19, x + butS);
    delay(500);
  }
  else if (butUpv == HIGH && butS > 0) {
    butS--;
    lcd.setCursor(19, x + butS);
    delay(500);
  }
  else {

  }

  if (butOKv == HIGH && butS == 0) {
    state = 8;
    Timechange();
    delay(500);
  }
  else if (butOKv == HIGH && butS == 1) {
    state = 9;
    TimeSet();
    delay(500);
  }
  else if (butOKv == HIGH && butS == 2) {
    state = 10;
    LED();
    delay(500);
  }
  else {

  }

  delay(500);
}

void Timechange() {
  int butDownv = digitalRead(butDown);
  int butUpv = digitalRead(butUp);
  int butOKv = digitalRead(butOK);
  int butSetv = digitalRead(butSet);
  if (buttle1 == 0) {
    h = rtc.getHour();
    m = rtc.getMinute();
    s = rtc.getSecond();
    lcd.clear();
    buttle1++;
    tbut = 0;
    x = 0;
    y = 0;
    lcd.setCursor(0, 0);
    lcd.print(h);
    lcd.setCursor(0, 1);
    lcd.print(m);
    lcd.setCursor(0, 2);
    lcd.print(s);
    lcd.setCursor(0, 3);
    lcd.print("Exit");
    
    //rtc.initClock();
  }
  
    
  if (tbut == 0) {
    lcd.setCursor(2, 0);
    if (butDownv == HIGH) {
      if (h != 23) {
        h++;

      }
      else {
        h = 0;
        lcd.setCursor(1, 0);
        lcd.print(" ");
      }
      lcd.setCursor(0, 0);
      lcd.print(h);
      delay(500);
    }
    if (butUpv == HIGH) {
      if (h != 0) {
        
        if(h==10){
        lcd.setCursor(1, 0);
        lcd.print(" ");
      }
      h--;
      }
      
      else {
        h = 23;
      }
      lcd.setCursor(0, 0);
      lcd.print(h);
      delay(500);
    }
   
  }

  
  if (tbut == 1) {
    lcd.setCursor(2, 1);
    if (butDownv == HIGH) {
      if (m != 60) {
        m++;

      }
      else {
        m = 0;
        lcd.setCursor(1, 1);
        lcd.print(" ");
      }
      lcd.setCursor(0, 1);
      lcd.print(m);
      delay(500);
    }
    if (butUpv == HIGH) {
      if (m != 0) {
        if(m==10){
        lcd.setCursor(1, 1);
        lcd.print(" ");
      }
        m--;
      }
      else {
        m = 60;
      }
      lcd.setCursor(0, 1);
      lcd.print(m);
      delay(500);
    }
    
  }

   if (tbut == 2) {
    lcd.setCursor(2, 2);
    if (butDownv == HIGH) {
      if (s != 60) {
        s++;

      }
      else {
        s = 0;
        lcd.setCursor(1, 2);
        lcd.print(" ");
      }
      lcd.setCursor(0, 2);
      lcd.print(s);
      delay(500);
    }
    if (butUpv == HIGH) {
      if (s != 0) {
        if(s==10){
        lcd.setCursor(1, 2);
        lcd.print(" ");
      }
        s--;
      }
      else {
        s = 60;
      }
      lcd.setCursor(0, 2);
      lcd.print(s);
      delay(500);
    }
    
  }
   if (tbut == 3) {
    lcd.setCursor(0, 3);
    if (butOKv == HIGH){
      //rtc.initClock();
      rtc.setTime(h,m,s);
      state=0;
    }
    
   }
 
  if (butSetv == HIGH) {
    tbutt++;      
  }
  if (butOKv == HIGH&&tbutt>0) {
      tbut++;
      tbutt=0;
  }
      
    

}

void TimeSet() {
  int butDownv = digitalRead(butDown);
  int butUpv = digitalRead(butUp);
  int butOKv = digitalRead(butOK);
  int butSetv = digitalRead(butSet);
  if (buttle1 == 0) {
    h = 0;
    m = 0;
    lcd.clear();
    buttle1++;
    tbut = 0;
    x = 0;
    y = 0;
    lcd.setCursor(0, 0);
    lcd.print(h);
    lcd.setCursor(0, 1);
    lcd.print(m);
    lcd.setCursor(0, 2);
    lcd.print("Exit");
    
    //rtc.initClock();
  }
  
    
  if (tbut == 0) {
    lcd.setCursor(2, 0);
    if (butDownv == HIGH) {
      if (h != 23) {
        h++;

      }
      else {
        h = 0;
        lcd.setCursor(1, 0);
        lcd.print(" ");
      }
      lcd.setCursor(0, 0);
      lcd.print(h);
      delay(500);
    }
    if (butUpv == HIGH) {
      if (h != 0) {
        
        if(h==10){
        lcd.setCursor(1, 0);
        lcd.print(" ");
      }
      h--;
      }
      
      else {
        h = 23;
      }
      lcd.setCursor(0, 0);
      lcd.print(h);
      delay(500);
    }
   
  }

  
  if (tbut == 1) {
    lcd.setCursor(2, 1);
    if (butDownv == HIGH) {
      if (m != 60) {
        m++;

      }
      else {
        m = 0;
        lcd.setCursor(1, 1);
        lcd.print(" ");
      }
      lcd.setCursor(0, 1);
      lcd.print(m);
      delay(500);
    }
    if (butUpv == HIGH) {
      if (m != 0) {
        if(m==10){
        lcd.setCursor(1, 1);
        lcd.print(" ");
      }
        m--;
      }
      else {
        m = 60;
      }
      lcd.setCursor(0, 1);
      lcd.print(m);
      delay(500);
    }
    
  }

   if (tbut == 2) {
    lcd.setCursor(0, 2);
    if (butOKv == HIGH){
      //rtc.initClock();
      cat=h*100+m;
      state=0;
    }
    
   }
 
  if (butSetv == HIGH) {
    tbutt++;      
  }
  if (butOKv == HIGH&&tbutt>0) {
      tbut++;
      tbutt=0;
  }
      
    

}

void LED() {
  int butDownv = digitalRead(butDown);
  int butUpv = digitalRead(butUp);
  int butOKv = digitalRead(butOK);
  if (buttle3 == 0) {
    buttle3++;
    lcd.clear();
    butS3 = 0;
    x = 0;
    y = 0;
    lcd.setCursor(0, 0);
    lcd.print("Open");
    lcd.setCursor(0, 1);
    lcd.print("Close");
    lcd.setCursor(0, 2);
    lcd.print("Exit");
    lcd.setCursor(y + 19, x);
  }
  if (butDownv == HIGH && butS3 < 2) {
    butS3++;
    lcd.setCursor(19, x + butS3);
    delay(500);
  }
  else if (butDownv == HIGH && butS3 >= 2) {
    butS3 = 0;
    lcd.setCursor(19, x + butS3);
    delay(500);
  }
  else if (butUpv == HIGH && butS3 <= 0) {
    butS3 = 2;
    lcd.setCursor(19, x + butS3);
    delay(500);
  }
  else if (butUpv == HIGH && butS3 > 0) {
    butS3--;
    lcd.setCursor(19, x + butS3);
    delay(500);
  }
  else {

  }

  if (butOKv == HIGH && butS3 == 0) {
    //   buttle0=0;
    //   state=1;
    ledoc = true;
    delay(500);
  }
  else if (butOKv == HIGH && butS3 == 1) {
    //  buttle0=0;
    //  state=1;
    ledoc = false;
    delay(500);
  }
  else if (butOKv == HIGH && butS3 == 2) {
    buttle0 = 0;
    state = 1;
    delay(500);
  }
  else {

  }
  delay(500);
}

void GameState() {
  int butDownv = digitalRead(butDown);
  int butUpv = digitalRead(butUp);
  int butOKv = digitalRead(butOK);
  if (buttle4 == 0) {
    buttle4++;
    lcd.clear();
    butS4 = 0;
    x = 0;
    y = 0;
    lcd.setCursor(0, 0);
    lcd.print("RPS");
    lcd.setCursor(0, 1);
    lcd.print("???");
    lcd.setCursor(0, 2);
    lcd.print("Exit");
    lcd.setCursor(19, 0);
  }
  if (butDownv == HIGH && butS4 < 2) {
    butS4++;
    lcd.setCursor(19, x + butS4);
    delay(500);
  }
  else if (butDownv == HIGH && butS4 >= 2) {
    butS4 = 0;
    lcd.setCursor(19, x + butS4);
    delay(500);
  }
  else if (butUpv == HIGH && butS4 <= 0) {
    butS4 = 2;
    lcd.setCursor(19, x + butS4);
    delay(500);
  }
  else if (butUpv == HIGH && butS4 > 0) {
    butS4--;
    lcd.setCursor(19, x + butS4);
    delay(500);
  }
  else {

  }

  if (butOKv == HIGH && butS4 == 0) {
    state = 100;
    RPS();
    delay(500);
  }
  else if (butOKv == HIGH && butS4 == 1) {

    delay(500);
  }
  else if (butOKv == HIGH && butS4 == 2) {
    buttle4 = 0;
    state = 2;
    delay(500);
  }
  else {

  }
  delay(500);

}

void AlarmState() {
  lcd.clear();
  digitalWrite(buz, HIGH);
  
  lcd.print("Now AlarmState");

  delay(500);
}



void mic(int micV) { //感測麥克風並使用蜂鳴器
  if (micV > 250) {
    digitalWrite(buz, HIGH);
    //lcd.println("Y");
    delay(500);
  }
  else {
    digitalWrite(buz, LOW);
    delay(500);
    //lcd.println("N");
  }
}




void RPS() {
  int butDownv = digitalRead(butDown);
  int butUpv = digitalRead(butUp);
  int butOKv = digitalRead(butOK);
  int butSetv = digitalRead(butSet);
  int randomv = 0;
  if (gmaeint1 == 0) {
    randomv = random(49) % 3;
    gmaeint1++;
    lcd.clear();
    lcd.setCursor(0, 0);
    lcd.print("RPS");
    lcd.setCursor(2, 1);
    gmaemun1 = 0;
  }

  if ( butSetv == HIGH && Mandatory == 0 ) {
    state = 0;
  }
  
  if (gmaemun1 == 0) {
    lcd.setCursor(2, 1);
    lcd.print("R");
  }
  else if (gmaemun1 == 1) {
    lcd.setCursor(2, 1);
    lcd.print("P");
  }
  else if (gmaemun1 == 2) {
    lcd.setCursor(2, 1);
    lcd.print("S");
  }
  else {


  }

  if (butDownv == HIGH && gmaemun1 < 2) {
    gmaemun1++;
    delay(500);

  }
  else if (butDownv == HIGH && gmaemun1 >= 2) {
    gmaemun1 = 0;

    delay(500);
  }
  else if (butUpv == HIGH && gmaemun1 <= 0) {
    gmaemun1 = 2;

    delay(500);
  }
  else if (butUpv == HIGH && gmaemun1 > 0) {
    gmaemun1--;

    delay(500);
  }
  else {

  }

  if (butOKv == HIGH) {
    if (gmaemun1 == randomv) {
      RPSM(randomv);
      lcd.setCursor(0, 3);
      lcd.print("pase");
      delay(1000);
      gmaeint1 = 0;
      RPS();
      randomv = random(50) % 3;
    }
    else if (gmaemun1 == 0 && randomv == 1) {
      RPSM(randomv);
      lcd.setCursor(0, 3);
      lcd.print("player lose");
      delay(1000);

      gmaeint1 = 0;
      RPS();
      randomv = random(50) % 3;
    }


    else if (gmaemun1 == 1 && randomv == 2) {
      RPSM(randomv);
      lcd.setCursor(0, 3);
      lcd.print("player lose");
      delay(1000);

      gmaeint1 = 0;
      RPS();
      randomv = random(50) % 3;
    }



    else if (gmaemun1 == 1 && randomv == 0) {
      RPSM(randomv);
      lcd.setCursor(0, 3);
      lcd.print("player win");
      delay(1000);

      gmaeint1 = 0;
      RPS();
      randomv = random(50) % 3;
    }

    else if (gmaemun1 == 2 && randomv == 1) {
      RPSM(randomv);
      lcd.setCursor(0, 3);
      lcd.print("player lose");
      delay(1000);

      gmaeint1 = 0;
      RPS();
      randomv = random(50) % 3;
    }




    else if (gmaemun1 == 2 && randomv == 0) {
      RPSM(randomv);
      lcd.setCursor(0, 3);
      lcd.print("player lose");
      delay(1000);

      gmaeint1 = 0;
      RPS();
      randomv = random(50) % 3;
    }
    else if (gmaemun1 == 0 && randomv == 2) {
      RPSM(randomv);
      lcd.setCursor(0, 3);
      lcd.print("player win");
      delay(1000);
      state = 2;
      MainState(state);
    }
    else
    {

    }
    delay(1000);
  }

  delay(500);
}

void RPSM(int a) {
  if (a == 0) {
    lcd.setCursor(10, 1);
    lcd.print("R");
  }
  else if (a == 1) {
    lcd.setCursor(10, 1);
    lcd.print("P");
  }
  else if (a == 2) {
    lcd.setCursor(10, 1);
    lcd.print("S");
  }
  else {


  }

}



void superled() { //三色led燈
  int blueValue = 0;
  int greenValue = 0;
  int redValue = 0;

  for (int i = 1; i <= 100; i += 1) {
    redValue = random(1, 30);
    greenValue = random(1, 30);
    blueValue = random(1, 30);

    analogWrite(redPin, redValue);
    analogWrite(greenPin, greenValue);
    analogWrite(bluePin, blueValue);
    delay(10);
  }

  analogWrite(redPin, 0);
  analogWrite(greenPin, 0);
  analogWrite(bluePin, 0);
}



