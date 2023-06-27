using UnityEngine;
using System.Collections;

public class ElfManager : MonoBehaviour {
	public GameObject Elf;
	public string[] aniname;
	public GameObject[] ElfChar;
	public GameObject[] ElfWand;
	public GameObject[] Wand;
	public GameObject[] ElfPet;
	public GameObject[] Pet;
	public int iElf;
	public int iWand;
	public int iPet;
	public int iani;

	public void Reset() {
		iElf = 0;
		iWand = 0;
		iPet = 0;
		iani = 0;

		for(int i = 0; i < ElfWand.Length; i++) {
			ElfWand[i].SetActive(false);
			Wand[i].SetActive(false);
		}

		for(int i = 0; i < ElfPet.Length; i++) {
			ElfPet[i].SetActive(false);
			Pet[i].SetActive(false);
		}

		for(int i = 0; i< ElfChar.Length; i++) {
			ElfChar[i].SetActive(false);
		}

		Elf.GetComponent<Animation>().CrossFade(aniname[iani]);
		ElfChar[0].SetActive(true);
		Wand[0].SetActive(true);
		Pet[0].SetActive(true);
	}

	void OnGUI() {
		// Character
		GUI.Label(new Rect(10, 30, 100, 20),"Character");
		if(GUI.Button(new Rect(80, 30, 25, 25), "<")) {
			PreviousCharacter();
		}

		GUI.Label(new Rect(110, 30, 100, 20),ElfChar[iElf].name);

		if(GUI.Button(new Rect(160, 30, 25, 25), ">")) {
			NextCharacter();
		}

		// Animation
		GUI.Label(new Rect(10, 60, 100, 20),"Animation");
		if(GUI.Button(new Rect(80, 60, 25, 25), "<")) {
			PreviousAnimation();
		}

		GUI.Label(new Rect(110, 60, 100, 20),aniname[iani]);

		if(GUI.Button(new Rect(160, 60, 25, 25), ">")) {
			NextAnimation();
		}
		//Wand
		GUI.Label(new Rect(10, 90, 100, 20),"Wand");

		if(GUI.Button(new Rect(80, 90, 25, 25), "<")) {
			prevWand();
		}

		GUI.Label(new Rect(110, 90, 100, 20),Wand[iWand].name);

		if(GUI.Button(new Rect(160, 90, 25, 25), ">")) {
			NextWand();
		}

		// Pet
		GUI.Label(new Rect(10, 120, 100, 20),"Pet");
		if(GUI.Button(new Rect(80, 120, 25, 25), "<")) {
			prevPet();
		}

		GUI.Label(new Rect(110, 120, 100, 20),Pet[iPet].name);

		if(GUI.Button(new Rect(160, 120, 25, 25), ">")) {
			NextPet();
		}

		if (GUI.Button(new Rect(10, 150, 60, 25), "Reset")) {
			Reset();
		}
	}

	// Character
	private void PreviousCharacter() {
		iElf--;

		if(iElf < 0) iElf = ElfChar.Length - 1;

		for(int i = 0; i < ElfChar.Length; i++) {
			if(iElf != i) {
				ElfChar[i].SetActive(false);
			}
		}

		ElfChar [iElf].SetActive (true);
	}

	private void NextCharacter() {
		iElf++;

		if(iElf >= ElfChar.Length) iElf = 0;

		for(int i = 0; i < ElfChar.Length; i++) {
			if(iElf != i) {
				ElfChar[i].SetActive(false);
			}
		}

		ElfChar [iElf].SetActive (true);
	}

	// Animation
	private void PreviousAnimation() {
		iani--;

		if(iani < 0) iani = aniname.Length - 1;
		
		if(iani < 1) {
			for(int i = 0; i < ElfWand.Length; i++) {
				ElfWand[i].SetActive(false);
			}

			for(int i = 0; i < ElfPet.Length; i++) {
				ElfPet[i].SetActive(false);
			}
		} else if(iani > 0) {
			for(int i = 0; i < ElfWand.Length; i++) {
				if(iWand != i) {
					ElfWand[i].SetActive(false);
				}
			}

			for(int i = 0; i < ElfPet.Length; i++) {
				if(iPet != i) {
					ElfPet[i].SetActive(false);
				}
			}

			for(int i = 0; i< ElfChar.Length; i++) {
				if(iElf != i) {
					ElfChar[i].SetActive(false);
				}
			}

			ElfWand[iWand].SetActive(true);
			ElfPet[iPet].SetActive(true);
		}

		ElfChar[iElf].SetActive(true);
		Elf.GetComponent<Animation>().CrossFade(aniname[iani]);
	}

	private void NextAnimation() {
		iani++;

		if(iani >= aniname.Length) iani = 0;
		
		if(iani < 1) {
			for(int i = 0; i < ElfWand.Length; i++) {
				ElfWand[i].SetActive(false);
			}

			for(int i = 0; i < ElfPet.Length; i++) {
				ElfPet[i].SetActive(false);
			}
		} else if(iani > 0) {
			for(int i = 0; i < ElfWand.Length; i++) {
				if(iWand != i) {
					ElfWand[i].SetActive(false);
				}
			}

			for(int i = 0; i < ElfPet.Length; i++) {
				if(iPet != i) {
					ElfPet[i].SetActive(false);
				}
			}

			for(int i = 0; i< ElfChar.Length; i++) {
				if(iElf != i) {
					ElfChar[i].SetActive(false);
				}
			}

			ElfWand[iWand].SetActive(true);
			ElfPet[iPet].SetActive(true);
		}

		ElfChar[iElf].SetActive(true);
		Elf.GetComponent<Animation>().CrossFade(aniname[iani]);
	}

	// Wand
	private void prevWand() {
		iWand--;

		if(iWand < 0) iWand = Wand.Length - 1;

		for(int i = 0; i < Wand.Length; i++) {
			if(iWand != i) {
				Wand[i].SetActive(false);
			}
		}

		if(iani > 0) {
			for(int i = 0; i < ElfWand.Length; i++) {
				if(iWand != i) {
					ElfWand[i].SetActive(false);
				}
			}

			for(int i = 0; i < ElfPet.Length; i++) {
				if(iPet != i) {
					ElfPet[i].SetActive(false);
				}
			}

			for(int i = 0; i< ElfChar.Length; i++) {
				if(iElf != i) {
					ElfChar[i].SetActive(false);
				}
			}

			ElfWand[iWand].SetActive(true);
			ElfPet[iPet].SetActive(true);
		}

		Wand[iWand].SetActive (true);
	}

	private void NextWand() {
		iWand++;

		if(iWand >= Wand.Length) iWand = 0;

		for(int i = 0; i < Wand.Length; i++) {
			if(iWand != i) {
				Wand[i].SetActive(false);
			}
		}

		if(iani > 0) {
			for(int i = 0; i < ElfWand.Length; i++) {
				if(iWand != i) {
					ElfWand[i].SetActive(false);
				}
			}

			for(int i = 0; i < ElfPet.Length; i++) {
				if(iPet != i) {
					ElfPet[i].SetActive(false);
				}
			}

			for(int i = 0; i< ElfChar.Length; i++) {
				if(iElf != i) {
					ElfChar[i].SetActive(false);
				}
			}

			ElfWand[iWand].SetActive(true);
			ElfPet[iPet].SetActive(true);
		}

		Wand[iWand].SetActive (true);
	}
	
	// Broom
	private void prevPet() {
		iPet--;

		if(iPet < 0) iPet = Pet.Length - 1;

		for(int i = 0; i < Pet.Length; i++) {
			if(iPet != i) {
				Pet[i].SetActive(false);
			}
		}
		if(iani > 0) {
			for(int i = 0; i < ElfWand.Length; i++) {
				if(iWand != i) {
					ElfWand[i].SetActive(false);
				}
			}

			for(int i = 0; i < ElfPet.Length; i++) {
				if(iPet != i) {
					ElfPet[i].SetActive(false);
				}
			}

			for(int i = 0; i< ElfChar.Length; i++) {
				if(iElf != i) {
					ElfChar[i].SetActive(false);
				}
			}

			ElfWand[iWand].SetActive(true);
			ElfPet[iPet].SetActive(true);
		}

		Pet[iPet].SetActive (true);
	}

	private void NextPet() {
		iPet++;

		if(iPet >= Pet.Length) iPet = 0;

		for(int i = 0; i < Pet.Length; i++) {
			if(iPet != i) {
				Pet[i].SetActive(false);
			}
		}

		if(iani > 0) {
			for(int i = 0; i < ElfWand.Length; i++) {
				if(iWand != i) {
					ElfWand[i].SetActive(false);
				}
			}

			for(int i = 0; i < ElfPet.Length; i++) {
				if(iPet != i) {
					ElfPet[i].SetActive(false);
				}
			}

			for(int i = 0; i< ElfChar.Length; i++) {
				if(iElf != i) {
					ElfChar[i].SetActive(false);
				}
			}

			ElfWand[iWand].SetActive(true);
			ElfPet[iPet].SetActive(true);
		}

		Pet[iPet].SetActive (true);
	}
}