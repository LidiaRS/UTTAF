//Bibliotecas
#include <SPI.h>
#include <MFRC522.h>

//Pinos
#define BUZZER 7
#define LED_Green 6
#define LED_Red 5
#define SS_PIN 10
#define RST_PIN 9

String IDtag = ""; //Variável que armazenará o ID da Tag
String CardStop = "CARD_STOP_AQUI";
String PositiveCards[] = { "CARD_P", "CARD_P" };
String NegativeCards[] = { "CARD_N", "CARD_N", "CARD_N" };

MFRC522 ReaderRFID(SS_PIN, RST_PIN);

void setup()
{
  Serial.begin(115200);             // Inicializa a comunicação Serial
  SPI.begin();                      // Inicializa comunicacao SPI
  LeitorRFID.PCD_Init();            // Inicializa o leitor RFID
  pinMode(LED_Green, OUTPUT);       // Declara o pino do led verde como saída
  pinMode(LED_Red, OUTPUT);    // Declara o pino do led vermelho como saída
  pinMode(BUZZER, OUTPUT);          // Declara o pino do buzzer como saída
}

void loop()
{
  Reader();  //Chama a função responsável por fazer a leitura das Tag's
}

void Reader()
{
        IDtag = ""; //Inicialmente IDtag deve estar vazia.

        // Verifica se existe uma Tag no leitor
        if ( !ReaderRFID.PICC_IsNewCardPresent() || !ReaderRFID.PICC_ReadCardSerial() )
        {
            delay(50);
            return;
        }

        // Pega o ID da Tag através da função LeitorRFID.uid e Armazena o ID na variável IDtag
        for (byte i = 0; i < ReaderRFID.uid.size; i++)
            IDtag.concat(String(ReaderRFID.uid.uidByte[i], HEX));

        acessoLiberado();

        delay(5000); //aguarda 5 segundos para efetuar uma nova leitura
}

void acessoLiberado()
{
      Serial.print(IDtag); //Exibe a mensagem "Tag Cadastrada" e o ID da tag não cadastrada

      if(IDtag != CardStop)
      {
          efeitoPermitido();  //Chama a função efeitoPermitido().
          CartaoPositivo();
          CartaoNegativo();
          Stop();
      }
}

void Stop()
{
      if(IDtag == CardStop)
      {
        digitalWrite(LED_Green, LOW);
        digitalWrite(LED_Red, LOW);
      }
}

void CartaoPositivo(){
  for(int i = 0; i < 2; i++)
  {
    if(PositiveCards[i] == IDtag)
    {
      digitalWrite(LED_Red, LOW);
      digitalWrite(LED_Green, HIGH);
    }
  }
}

void CartaoNegativo(){
  for(int i = 0; i < 3; i++)
  {
    if(NegativeCards[i] == IDtag)
    {
      digitalWrite(LED_Green, LOW);
      digitalWrite(LED_Red, HIGH);
    }
  }
}

void efeitoPermitido(){
  int qtd_bips = 1; //definindo a quantidade de bips
  for(int j=0; j<qtd_bips; j++)
  {
    //Ligando o buzzer com uma frequência de 1500 hz e ligando o led verde.
    tone(BUZZER,1500);
    delay(100);

    //Desligando o buzzer e led verde.
    noTone(BUZZER);
    delay(100);
  }
}