﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBackground : MonoBehaviour {
	private const int quantAlas = 3;
	private const int quantElementosUI = 4;
	public GameObject[] backgrounds = new GameObject[quantAlas];
	public GameObject[] molduras = new GameObject[quantAlas];
	public GameObject[] termometros = new GameObject[quantElementosUI];
	public GameObject[] alavancasUI = new GameObject[quantElementosUI];
	private GameObject[] elementosTemperatura1 = new GameObject[21];
	private GameObject[] elementosTemperatura2 = new GameObject[22];
	private GameObject[] elementosTemperatura3 = new GameObject[22];
	private ModalController modal;
	private int alavancasAtivadas;

	// Use this for initialization
	void Start () {
		elementosTemperatura1 = GameObject.FindGameObjectsWithTag ("ElementosTemperatura1");
		elementosTemperatura2 = GameObject.FindGameObjectsWithTag ("ElementosTemperatura2");
		elementosTemperatura3 = GameObject.FindGameObjectsWithTag ("ElementosTemperatura3");

		//inicializa backgrounds
		inicializarElementosBackground(backgrounds);
		//inicializa molduras
		inicializarElementosBackground(molduras);
		//inicializa paredes, pisos e tetos
		inicializarElementosMolduraInterna();
		//inicializa elementos UI
		inicializarElementosBackground(termometros);
		inicializarElementosBackground (alavancasUI);

		alavancasAtivadas = 0;
		modal = GameObject.Find ("Managers").GetComponent<ModalController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void mudarCenario(int idAlavanca){
		if (idAlavanca > 0) {
			alavancasAtivadas++;
			//atualiza elementos UI
			alterarElementosUI(idAlavanca);
		
			if (idAlavanca < quantAlas) {
				for (int i = 0; i < quantAlas; i++) {
					if (i != idAlavanca) {
						backgrounds [i].SetActive (false);
						molduras [i].SetActive (false);
					}
				}
				backgrounds [idAlavanca].SetActive (true);
				molduras [idAlavanca].SetActive (true);
				alterarTemperaturaElementosAla (idAlavanca);	
			}
		}	
	}

	public void alterarTemperaturaElementosAla(int idAlavanca){
		if (idAlavanca == 1) {
			alterarElementosMolduraInterna (elementosTemperatura1, false);
			alterarElementosMolduraInterna (elementosTemperatura2, true);
			alterarElementosMolduraInterna (elementosTemperatura3, false);
		}
		if (idAlavanca == 2) {
			alterarElementosMolduraInterna (elementosTemperatura1, false);
			alterarElementosMolduraInterna (elementosTemperatura2, false);
			alterarElementosMolduraInterna (elementosTemperatura3, true);
		}
	}
		
	public void inicializarElementosBackground(GameObject[] elementos){
		for (int i = 1; i < elementos.Length; i++) {
			elementos [i].SetActive (false);
		}
		elementos [0].SetActive (true);
	}

	public void inicializarElementosMolduraInterna(){
		alterarElementosMolduraInterna (elementosTemperatura1, true);
		alterarElementosMolduraInterna (elementosTemperatura2, false);
		alterarElementosMolduraInterna (elementosTemperatura3, false);
	}

	public void alterarElementosMolduraInterna(GameObject[] elementosTemperaturaX, bool visibilidade){
		for (int i = 0; i < elementosTemperaturaX.Length; i++) {
			elementosTemperaturaX [i].SetActive (visibilidade);
		}
	}
		
	public void alterarElementosUI(int idAlavanca){
		if (idAlavanca == 3) {
			termometros [3].SetActive (true);
			alavancasUI [3].SetActive (true);
			alavancasUI [2].SetActive (false);
		} else {
			for (int i = 0; i < quantElementosUI; i++) {
				if (i == idAlavanca) {
					termometros [i].SetActive (true);
					alavancasUI [i].SetActive (true);
				} else {
					termometros [i].SetActive (false);
					alavancasUI [i].SetActive (false);
				}
			}	
		}
	}

	public int getQuantAlas(){
		return ManagerBackground.quantAlas;
	}
		
	public int getAlavancasAtivadas(){
		return this.alavancasAtivadas;
	}

}

