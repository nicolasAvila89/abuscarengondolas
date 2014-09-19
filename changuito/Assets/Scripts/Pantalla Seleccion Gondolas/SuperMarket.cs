﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SuperMarket : MonoBehaviour
{

	void Start ()
	{
        if (!seleccionFinalizada())
        {
            ListadoSingleton.Instance.initializeListado();
            createGondolas();
            setTargetOfChanguito();
            setProductTarget();
        }
        else
        {
            finalizarJuego();
        }
    }

    private void finalizarJuego()
    {
        if (ChanguitoConfiguration.ModuloPago)
        {
            Application.LoadLevel("PantallaPago");
        }
        else
        {
            Application.LoadLevel("PantallaFinal");
        }
    }

    private Boolean seleccionFinalizada(){

       return ChanguitoConfiguration.CantidadGondolas == ListadoSingleton.PosicionActual;
    }

    private void setProductTarget()
    {
        ListadoSingleton.Target = ListadoSingleton.Instance.getTarget();
    }

    private void setTargetOfChanguito()
    {
       GameObject changuito = GameObject.Find("Changuito");
       changuito.GetComponent<ChanguitoTarget>().Gondola = ListadoSingleton.Instance.getTargetType();
    }

	private void createGondolas ()
	{		
        GameObject gondolasOnScene = GameObject.Find("SGGondolasTable");
        
        int gondolaPosition;
        ArrayList gondolasOrdenadas = new ArrayList(ChanguitoConfiguration.CantidadGondolas);
        for (gondolaPosition = 0; gondolaPosition < ChanguitoConfiguration.CantidadGondolas; gondolaPosition++)
        {
            GameObject gondola = (GameObject)Resources.Load("Gondola");
            String name = ListadoSingleton.Instance.getLabelOfGondolaType(gondolaPosition);
            gondola.GetComponent<UILabel>().text = name;
            gondola.name = name; 
            gondola.GetComponent<GondolaProperties>().ProductType = (int)ListadoSingleton.Instance.getGondolasSeleccionadas()[gondolaPosition];
            
            NGUITools.AddChild(gondolasOnScene, gondola);
         }

        gondolasOnScene.GetComponent<UITable>().Reposition();
    }

 }