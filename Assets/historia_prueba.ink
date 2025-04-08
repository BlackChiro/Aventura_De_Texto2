//variables globales, se usan en cualquier sitio
VAR NombreJugador = "Black"

VAR ElegidoEquipoPerros = false

VAR PuntosVida = 50
EXTERNAL ChangeMood(CharacterName, mood)
EXTERNAL ShowCharacter(characterName, position, mood)
EXTERNAL HideCharacter(characterName)
EXTERNAL SwitchSong()

{ShowCharacter("Acolito", "Center", "Sentado")}

->nudo1 //esto es donde empeza la historia

== nudo1 ==
Buenas {NombreJugador}
Mi nombre es Magnus y soy mienbro de los acolitos del sol
-> nudo2

== nudo2 ==
{ChangeMood("Acolito","PensandoPensamientos")}
Eres de los nuestros?
o estas contra nosotros?
*** adorar al sol
~ElegidoEquipoPerros = true //actualizo la var
->Equipo_Sol

*** Enfrentarse
~PuntosVida -= 20
->Equipo_NoSol


== Equipo_Sol ==

Te mostrare la sabiduria del gran sol
->Continuacion_conver

== Equipo_NoSol ==
{ChangeMood("Acolito"," Maldecir")}
Te mato aqui ahora mismo cabron
->Continuacion_conver

== Continuacion_conver ==
{SwitchSong()}
{ PuntosVida < 50: 
{ChangeMood("Acolito"," Maldecir")}
}
Seras una gran ofrenda
{Equipo_Sol: 
{ChangeMood("Acolito","PensandoPensamientos")}
Sigueme
}
{not Equipo_Sol: -> No_equipo_Sol}
->END

== No_equipo_Sol ==
{HideCharacter("Acolito")}
"Mantuviste el buen camino"
->END

