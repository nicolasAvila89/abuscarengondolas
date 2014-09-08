﻿using UnityEngine;
using System.Collections;
using System;

public class SaveConfiguration : MonoBehaviour {


    void OnClick()
    {
        this.saveCantidadProductos();
        this.saveModuloPago();
        this.saveModuloControlVuelto();
    }

    private void saveModuloPago()
    {
        GameObject check = GameObject.Find("ModuloPagoCheckbox");
        ChanguitoConfiguration.moduloPago = check.GetComponent<UIToggle>().value;
    }

    private void saveModuloControlVuelto()
    {
        GameObject check = GameObject.Find("ModuloControlVueltoCheckbox");
        ChanguitoConfiguration.moduloControlVuelto = check.GetComponent<UIToggle>().value;
    }

    private void saveCantidadProductos()
    {
        GameObject popup = GameObject.Find("CantidadProductosPopupList");
        ChanguitoConfiguration.cantidadGondolas = Int32.Parse(popup.GetComponent<UIPopupList>().value);
    }
}
