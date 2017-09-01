# DrawForms
C#

Read me 

Projeto DrawForms

Esse projeto consiste em desenhar uma forma geométrica e aplicar transformações nela.
As transformações possíveis são: 
Escalonar o polígono, definido de 1 até 2.5x.
Rotacionar o polígono no seu próprio eixo, de 0° a 360°.
Movimentar o polígono utilizando teclas do teclado.

Para a entrada de dados do polígono, foi utilizado um arquivo texto com as coordenadas dos pontos dele. O projeto já está abrindo um arquivo teste.txt para verificar sua funcionalidade. Este arquivo está no diretório do projeto.

Componentes de tela:
Para esse projeto foi utilizado dois “trackbar" do Form c# principal.
Utilizado para que tenha limites na escala e na rotação do polígono citados anteriormente.
Também neste Form é tratado os eventos do teclado, para que o polígono possa se movimentar sobre a tela. As teclas foram definidas desta forma:
W > Movimenta para cima.
A > Movimenta para esquerda.
S > Movimenta para baixo.
D > Movimenta para direita.

