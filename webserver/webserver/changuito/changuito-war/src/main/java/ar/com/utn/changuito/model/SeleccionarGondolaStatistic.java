package ar.com.utn.changuito.model;

import ar.com.utn.changuito.architecture.net.SharedObject;

public class SeleccionarGondolaStatistic extends Statistic{

	public void setFailedGondolas(final int failedGondolas){
		set("failedGondolas",failedGondolas);
	}
	
	public int getFailedGondolas(){
		return getInt("failedGondolas");
	}

	public void setCantidadGondolas(final int cantidadGondolas){
		set("cantidadGondolas",cantidadGondolas);
	}
	
	public int getCantidadGondolas(){
		return getInt("cantidadGondolas");
	}

	public SeleccionarGondolaStatistic(){
		
	}
	
   public SeleccionarGondolaStatistic(final SharedObject sharedObject) {
	        super(sharedObject);
   }
}
