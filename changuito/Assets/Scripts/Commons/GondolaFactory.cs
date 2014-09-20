﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// PseudoFabrica de gondolas
/// Preestablecemos 8 tipos de gondolas.
///    0 - Verduras: Lechuga, Cebolla, Tomate, Zapallo, Papa, Choclo, Zanahoria
///    1 - Frutas: Manzana, Banana, Pera, Naranja, Sandía
///    2 - Bebidas: Agua, Jugo, Gaseosa, Soda
///    3 - Golosinas: Chicle, Alfajor, Chupetin, Caramelo, Chocolate
///    4 - Almacén: Fideos, Aceite, Pan, Mermelada, Galletitas, Azúcar, Sal, Cereales
///    5 - Frescos: Huevos, Postrecitos, Salchichas, Jamón
///    6 - Lácteos: Leche, Queso, Yogurt, Manteca, Queso rallado
///    7 - Perfumería: Shampoo, Jabón, Desodorante, Dentífrico, Cepillo de dientes
/// </summary>
/// 
public class GondolaFactory{

    public static int MAX_PRODUCTOS_X_TIPO = 4;

    public static int VERDURAS = 0;
    public static int FRUTAS = 1;
    public static int BEBIDAS = 2;
    public static int GOLOSINAS = 3;
    public static int ALMACEN = 4;
    public static int FRESCOS = 5;
    public static int LACTEOS = 6;
    public static int PERFUMERIA = 7;

    private static IDictionary<int, string> tipoGondolasDictionary = new Dictionary<int, string>(){
        {VERDURAS,"Verduras"},
        {FRUTAS,"Frutas"},
        {BEBIDAS,"Bebidas"},
        {GOLOSINAS,"Golosinas"},
        {ALMACEN,"Almacen"},
        {FRESCOS,"Frescos"},
        {LACTEOS,"Lacteos"},
        {PERFUMERIA,"Perfumeria"}
    };

   public static  IDictionary<int, ArrayList> gondolasDictionary = new Dictionary<int, ArrayList>()
    {
        {VERDURAS, new ArrayList() { "Lechuga", "Cebolla", "Tomate", "Zapallo" }},
        {FRUTAS, new ArrayList() { "Manzana", "Banana", "Pera", "Naranja"}},
        {BEBIDAS, new ArrayList() { "Agua", "Jugo", "Gaseosa", "Soda" }},
        {GOLOSINAS, new ArrayList() { "Chicle", "Alfajor", "Chupetin", "Caramelo" }},
        {ALMACEN, new ArrayList() { "Fideos", "Aceite", "Pan", "Galletitas" }},
        {FRESCOS, new ArrayList() { "Huevos", "Flan", "Salchichas", "Jamón" }},
        {LACTEOS, new ArrayList() { "Leche", "Queso", "Yogurt", "Manteca"}},  
        {PERFUMERIA, new ArrayList() { "Shampoo", "Jabón", "Desodorante", "Dentífrico" }}
    };

    /// <summary>
    /// Devuelve todos sus productos de un tipo en particular.
    /// </summary>
    /// <param name="tipo"></param>
    /// <returns></returns>
    public static ArrayList getGondola(int tipo){
     
        ArrayList value = new ArrayList(4);
        gondolasDictionary.TryGetValue(tipo,out value);
        
        return value;    
    }


    /// <summary>
    /// Matcheo Tipo (int) -> Tipo (String)
    /// </summary>
    /// <param name="tipo"></param>
    /// <returns></returns>
    public static string getTipoGondola(int tipo)
    {
        string label;
        tipoGondolasDictionary.TryGetValue(tipo, out label);
        
        return label;
    }

}