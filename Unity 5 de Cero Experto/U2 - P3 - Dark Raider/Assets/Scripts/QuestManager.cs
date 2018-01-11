using UnityEngine;
using System.Collections;


[System.Serializable]
public class Quest
{
	public enum QUEST_STATUS {
		UNASSIGNED = 0,
		ASSIGNED = 1,
		COMPLETED = 2
	};

	public QUEST_STATUS status = QUEST_STATUS.UNASSIGNED;
	public string questName = string.Empty;

}





public class QuestManager : MonoBehaviour {

	public Quest[] quests;
	private static QuestManager manager = null;

	public static QuestManager managerInstance {


		get{
			if (manager == null) {
				GameObject questObject = new GameObject ("Default");
				manager = questObject.AddComponent<QuestManager> ();
			}

			return manager;
		}

	}


	void Awake() {
		if (manager) {
			DestroyImmediate (gameObject);
			return;
		}

		manager = this;
		DontDestroyOnLoad (manager);
	}



	public static Quest.QUEST_STATUS GetQuestStatus(string questName){

		foreach (Quest q in managerInstance.quests) {
			if (q.questName.Equals (questName)) {
				return q.status;
			}
		}

		return Quest.QUEST_STATUS.UNASSIGNED;

	}


	public static void SetQuestStatus(string questName, Quest.QUEST_STATUS newQuestStatus){

		foreach (Quest q in managerInstance.quests) {
			if (q.questName.Equals (questName)) {
				q.status = newQuestStatus;
				return;
			}
		}
	}


	public static void Reset(){
		if (managerInstance == null)
			return;

		foreach (Quest q in managerInstance.quests) {
			q.status = Quest.QUEST_STATUS.UNASSIGNED;
		}
	
	}



}
