# IAV21-William-Molina
 # Curso 2020 / 2021 - Universidad Complutense de Madrid
# Facultad de Informática - Videojuegos

Proyecto de Inteligencia Artifical.

# Proyecto
<a href="https://drive.google.com/file/d/1SXYW90SB00EwjEj-1DP_cQei4oAopQFV/view?usp=sharing" target="_blank">Video Demo</a>

<a href="https://drive.google.com/file/d/1CjIqRjm2vDu_tkiMd6S1iXas-vBaD6ln/view?usp=sharing" target="_blank">Descargar Build x86x64</a>

Planteamiento del proyecto
-----------------------------
El proyecto consiste en implementar el comportamiento de animales que conviven en un refugio. Estos animales se encuentran en diferentes islas y deben ser rescatados por el jugador para llevarlos al refugio. 
Una vez dentro, interactúan con otros animales para realizar distintas acciones, pero la acción principal consiste en el enfrentamiento de estos. 
Si un animal es hostil por naturaleza, buscara un enfrentamiento con otro animal de su mismo o menor nivel de peligrosidad. Si un animal busca alimento y decide que quiere cazar, buscará un animal con menor nivel de peligrosidad para iniciar un enfrentamiento y poder devorarlo.
De la misma forma, los animales más débiles y pacíficos huirán de su cazador para evitar el enfrentamiento.
Por último, para poder domesticar a los animales, se deberá alimentarlos con la comida correcta hasta ganarse su confianza y solo entonces se podrán llevar al refugio.

COMPORTAMIENTOS BÁSICOS DESARROLLADOS
-----------------------------
*Cada comportamiento cuanta con feedback visual que el jugador puede identificar al observar a un animal.*

Dormir: 
* Los animales pueden entrar en el estado de Dormir si no se encuentran realizando ninguna acción y decide que es seguro.

Comer: 
* Si un animal decide entrar en este estado, buscará comida en un radio alrededor, si la encuentra, identificará si es comestible por su especie y se acercará para comersela, en caso contrario, la ignorará.

Merodear: 
* Estado inicial para el animal, en el que, de forma aleatoria navegará por el mundo sorteando obstáculos y observando al jugador si este se encuentra cerca de su rango de visión.

Pelear: 
* Un animal entrará en este estado dependiendo de su naturaleza agresiva (principalmente su especie), identificando a un animal en su rango de visión y valorando el nivel de peligrosidad de la posible victima para poder iniciar el enfrentamiento.

Seguir a jugador: 
* Si el jugador se encuentra cerca, mediante gestión sensorial (como el resto de estados que tienen que ver con identificar a otra entidad), el animal puede entrar en un estado amigable en el que sigue al jugador (solo si el animal no es salvaje).

COMPORTAMIENTOS COMPLEJOS DESARROLLADOS
-----------------------------
Domesticarse:
* Un animal puede ser domesticado por el jugador, cuando el jugador viaja mediante el botón de exploración a una isla, y en esta encuentra a un animal salvaje. Para poder domesticarlo deberá alimentarlo con cierta cantidad de comida apropiada para el animal. Una vez domesticado el animal viajará con el jugador al refugio cuando este decida volver.

Detener pelea:
* Durante un enfrentamiento entre animales el jugador puede interactuar con ellos para detenerlos (botón E), de forma que los animales dejarán de pelear, volverán a un estado neutro y ganarán experiencia de aprendizaje.

Detectar amenaza:
* Los animales más vulnerables o con el menor nivel de peligrosidad, podrán tener la percepción de si otro animal de mayor peligrosidad les está atacando o tiene intención de ello, en ese caso huirán en la dirección contraria a su perseguidor, además de moverse en zig zag aleatoriamente para intentar perderlo lo antes posible.

Detectar presa:
* Así mismo, cuando un animal con un nivel de peligrosidad mayor encuentra a otro con menor nivel de peligrosidad, y está en el estado de buscar comida, podrá decidir atacar al animal más débil convirtiéndolo en su presa. Si no consigue escapar, lo devorará saciando su hambre.

Reproducirse:
* Un animal podrá entrar en este estado buscando un compañero de su misma especie, si lo encuentra, comunicará sus intenciones y su compañero se unirá a su estado de forma que ambos se acercarán. Después de unos segundos aparecerá un tercer animal de su misma especie como resultado de este estado.

CARACTERíSTICAS DE LOS ANIMALES
-----------------------------
*Cada animal cuenta con sus estadísticas específicas, puntos de vida máximos, puntos de experiencia máximos, velocidad de movimiento, puntos de daño...*

![peligrosidad](https://user-images.githubusercontent.com/43720307/120361108-c7414e80-c309-11eb-9478-4fbcdbc680a4.png)

![alimentacion](https://user-images.githubusercontent.com/43720307/120361942-c9f07380-c30a-11eb-880a-946f5e6bf8ab.png)

CONCEPTOS IMPORTANTES: Inventario, GoodBoy System, Domesticar...
-----------------------------
* Se debe recoger comida antes de iniciar una exploración, acercandose a los items y pulsando el botón [E] para agregarlos al inventario. Para acceder a este pulsar [TAB] y arrastrar los items del inventario a la fila inferior para poder usarlos in game en la hotbar.
* Para acceder a la exploración en busca de un animal salvaje, se debe abrir el menú de pausa [ESC] y darle al botón Explore.
* Una vez encontrado al animal, se lanza la comida correspondiente a su especie con botón [Q]. Una vez se complete la barra azul de domesticar el animal, este aparecerá en el refugio cuando el jugador vuelva.
* Ya en el refugio, el animal convivirá con el resto de animales rescatados en las exploraciones.
* Debajo de la barra de vida, los animales que pueden atacar (nivel de peligrosidad > 0) contarán además con una barra de experiencia, que se irá llenando cada vez que el jugador detenga un enfrentamiento de este con otro animal. Una vez completada esta barra, el animal finalizará su aprendizaje y ya no atacará al resto de animales en ningún momento dentro del refugio.
* También se puede limpiar el cadáver de un animal con el botón [E].

MÁQUINAS DE ESTADOS
-----------------------------
*La técnica de toma de decisiones utilizada, está basada en probabilidad. Se "tira un dado" para saber cual será la próxima acción, pero esto a su vez estará condicionado por las características del animal.*

ANIMAL SALVAJE

![maquina de estados2](https://user-images.githubusercontent.com/43720307/120366919-8436a980-c310-11eb-9a18-3b3dfbecf10c.png)

ANIMAL DOMESTICADO

![maquina de estados](https://user-images.githubusercontent.com/43720307/120366933-88fb5d80-c310-11eb-8fe0-32eea7151261.png)

Objetivos obligatorios desarrollados del proyecto
---------------------------------------------------
* Viajar con un botón para explorar una isla en busca de animales salvajes, y una vez encontrarlos, poder alimentarlos hasta conseguir domesticarlos.
* Observar el progreso al domesticar cada animal con una barra azul y los puntos de vida también representados con una barra.
* Ver como los animales pueden tener algo en mente y a partir de ahí desarrollar distintas decisiones dentro del refugio, para facilitar esto se debe añadir feedback visual como un símbolo que represente lo que se tiene en mente.
* Llevar una especie de mochila con alimentos para los animales y que, al tirarlos, estos puedan identificarlos (pueden no ser comestibles para una especie de animal, en ese caso el animal los ignora).
* Presenciar un enfrentamiento entre animales dentro del refugio, solo uno sale con vida y dependiendo de la motivación que ha llevado al enfrentamiento (por simple hostilidad o por hambre de uno de los animales), el animal victorioso devorará al vencido o simplemente dejará su cadáver.

Ampliaciones añadidas al proyecto
---------------------------------------------------
* Música de juego.
* Sonido para cada animal.
* Sistema de inventario complejo con hotbar o barra de navegación (rueda del ratón).
* 2 mapas complejos trabajados con la herramienta Polybrush.
* HUD y cursor personalizado del jugador.
* Menú principal y menú de pausa.
* PopUps de avisos, que saltan cuando se ha conseguido domesticar un animal y cuando se intenta ir a explorar mientras hay un conflicto en el refugio.
* Sistema de seleccion de items basado en raycast, por el jugador para poder recoger items e interactuar con los animales.
* Cámara en primera persona y control fluido del jugador.

Pruebas realizadas
---------------------------------------------------
<a href="https://drive.google.com/file/d/1fkqy6bpik8eZFNRFQw2I4Ks3BG5VVLDB/view?usp=sharing" target="_blank">Video de Prueba devorar</a>.

<a href="https://drive.google.com/file/d/1LJVliS4JpYdp0SLixB6XJGEQMsJMwFDd/view?usp=sharing" target="_blank">Video de Prueba enfrentamiento</a>.

<a href="https://drive.google.com/file/d/1UjdSUZuHRch12fQV851h2BZoK3nibinR/view?usp=sharing" target="_blank">Video de Prueba persecución</a>.

<a href="https://drive.google.com/file/d/1Fn0EMdOhYJjPs7-er4lHhj0qDcKfUTnu/view?usp=sharing" target="_blank">Video de Prueba reproducción</a>.

<a href="https://drive.google.com/file/d/13IRIRGoJl2qC-9zhDUxKGi6Odum3vZhO/view?usp=sharing" target="_blank">Video de Prueba domesticar</a>.

Assets y referencias
---------------------------------------------------
* AI for Games 3rd Edition (2019) - Ian Millington
* Referencias utilizadas para hacer uso de las diversas herramientas de Unity
* Unity Asset Store 
* [NavMeshAgents](https://www.youtube.com/watch?v=VJ2iW_laA-Y)
* [Bolt](https://www.youtube.com/watch?v=SVpkh3kMIcg)
* [PolyBrush](https://www.youtube.com/watch?v=JQyntL-Z5bM)
* ProBuilder
* ProGrids
* [Voxels](https://sketchfab.com/tags/voxel)
