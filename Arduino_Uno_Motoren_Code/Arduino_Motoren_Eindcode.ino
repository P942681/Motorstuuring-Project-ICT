// Motor A connections
const int enA = 9;
const int in1 = 8;
const int in2 = 7;

// Motor B connections
const int enB = 3;
const int in3 = 5;
const int in4 = 4;

int pwm;
int richting;

void setup() {
  pinMode(enB, OUTPUT);
  pinMode(in1, OUTPUT);
  pinMode(in2, OUTPUT);
  pinMode(in3, OUTPUT);
  pinMode(in4, OUTPUT);

  pwm = 0;
  richting = 0;

  Serial.begin(9600);
}

void loop() {
  // Data inlezen
  if (Serial.available() > 1) {
    pwm = Serial.parseInt();
    richting = Serial.parseInt();

    // de richting van de motoren bepalen
    switch (richting) {
      case 1: // Vooruit
        digitalWrite(in1, HIGH);
        digitalWrite(in2, LOW);
        digitalWrite(in3, HIGH);
        digitalWrite(in2, LOW);
        delay (200);
        break;

      case 2: // Achteruit
        digitalWrite(in1, LOW);
        digitalWrite(in2, HIGH);
        digitalWrite(in3, LOW);
        digitalWrite(in4, HIGH);
        delay (200);
        break;

      case 3: // Links
        digitalWrite(in1, LOW);
        digitalWrite(in2, HIGH);
        digitalWrite(in3, LOW);
        digitalWrite(in4, LOW);
        delay (200);
        break;

      case 4: // Rechts
        digitalWrite(in1, LOW);
        digitalWrite(in2, LOW);
        digitalWrite(in3, LOW);
        digitalWrite(in4, HIGH);
        delay (200);
        break;

      case 0: // Stop
        digitalWrite(in1, LOW);
        digitalWrite(in2, LOW);
        digitalWrite(in3, LOW);
        digitalWrite(in4, LOW);
        delay (200);
        break;

      default:
        digitalWrite(in1, LOW);
        digitalWrite(in2, LOW);
        digitalWrite(in3, LOW);
        digitalWrite(in4, LOW);
        break;
    }
  }

  // De snelheid aanpassen van de motoren
  analogWrite(enA, pwm);
  analogWrite(enB, pwm);
  delay (50);
}
