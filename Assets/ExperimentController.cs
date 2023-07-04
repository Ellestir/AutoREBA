using QuestionnaireToolkit.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ExperimentController : MonoBehaviour { 
    public enum OrderOptions { BalancedLatinSquare, LatinSquare, Permutations, Random };
    public enum GenderOptions { Male, Female, Other, DoNotWantToSpecify };
    public enum RecruitmentOption { Student, Staff, FamilyAndFriends, InvitedExternal, Other };

    private OrderOptions selectedOrder;

    [Header("Demographics")]
    public int SubjectID;
    public int Age;
    public GenderOptions Gender;
    public RecruitmentOption Recruitment;

    [Header("Experimental Conditions")]
    public GameObject[] conditions; // Click on "Conditions" in Inspector, enter # conditions, assign game objects
    public int currentCondition = 0;
    public string currentConditionName = "";

    [Header("Experimental Order of Conditions")]
    public OrderOptions ConditionOrder;

    [Header("Questionnaire Toolkit")]
    public GameObject QTQuestionnaireManager;
    private QTManager qtManager;
    private QTQuestionnaireManager qtQManager;
    private int currentQuestionnaireNumber = 0;
    private bool trialRunning;

    [Header("Logging Paths")]
    public string loggingPath = @"Assets/Results/Logs/";
    public string questionnairePath = @"Assets/Results/Questionnaires/";

    private StreamWriter fileWriter;


    void Start() {
        fileWriter = new StreamWriter(loggingPath + "eventLog_" + System.DateTime.Now.Ticks + ".csv");
        fileWriter.AutoFlush = true;
        fileWriter.WriteLine("TimeStamp;SubjectID;Gender;Age;Recruitment;currentConditionName;currentConditionNumber;HitObject");

        switch (selectedOrder) {
            case OrderOptions.BalancedLatinSquare:
                conditions = GetBalancedLatinSquare(conditions, SubjectID);
                break;

            case OrderOptions.LatinSquare:
                conditions = GetLatinSquare(conditions, SubjectID);
                break;

            case OrderOptions.Permutations:
                conditions = GetAllPermutations(conditions, SubjectID);
                break;

            case OrderOptions.Random:
                conditions = GetLatinSquare(conditions, SubjectID);
                break;
        }
        
        showCondition(0);

        
    }
     
    void Update() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            showQuestionnaire();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            previousCondition();
        }

        if (trialRunning) { 
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit raycastHit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out raycastHit, 100f)) {
                    if (raycastHit.transform != null) { 
                        logEvent(raycastHit.transform.gameObject);
                    }
                }
            }
        }
    }

    public string getCurrentConditionName() {
        return currentConditionName;
    }
    public int getCurrentCondition() {
        return currentCondition;
    }
    public int getSubjectID() {
        return SubjectID;
    }
    public int getAge() {
        return Age;
    }
    public string getGender() {
        return Gender.ToString();
    }
    public string getRecruitment() {
        return Recruitment.ToString();
    }

    public void nextQuestionnaire() {
        qtQManager.HideQuestionnaire();        
        currentQuestionnaireNumber++; 
        qtQManager = qtManager.questionnaires[currentQuestionnaireNumber];
        qtQManager.resultsSavePath = questionnairePath;
        qtQManager.StartQuestionnaire();
    }

    private void previousCondition() {
        if (currentCondition > 0) {
            currentCondition--;
            showCondition(currentCondition); 
        }
    }

    private void showCondition(int conditionID) {
        trialRunning = true;

        for (int i = 0; i < conditions.Length; i++) {
            conditions[i].SetActive(i == conditionID);
        } 
        currentConditionName = conditions[conditionID].name;
        print("Current Subject: " + SubjectID + "Current Condition: " + conditionID + " Condition Name: " + conditions[conditionID].name);
    }   
      
    private void stopRecording() {
        fileWriter.Close();
        fileWriter.Dispose();
    }

    private void logEvent(GameObject hitObject) {
        string fileOutput = System.DateTime.Now.Ticks + ";" + SubjectID + ";" + Gender + ";" + Age + ";" + Recruitment + ";" + currentCondition + ";" + currentConditionName + ";" + hitObject.name;
        print(fileOutput);
        fileWriter.WriteLine(fileOutput);
    }

    public void nextCondition() { 
        foreach(var questionnaire in qtManager.questionnaires) {
            questionnaire.ResetQuestionnaire();
            questionnaire.HideQuestionnaire();
        } 
        currentQuestionnaireNumber = 0;


        if (currentCondition < conditions.Length - 1) {
            currentCondition++;
            showCondition(currentCondition);
        } else {
            stopRecording();
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #elif UNITY_WEBPLAYER
                Application.OpenURL(webplayerQuitURL);
            #else
                Application.Quit();
            #endif
        }
    } 

    private void showQuestionnaire() {
        trialRunning = false;
        qtManager = QTQuestionnaireManager.GetComponent<QTManager>();
        qtQManager = qtManager.questionnaires[currentQuestionnaireNumber];
        qtQManager.resultsSavePath = questionnairePath;
        qtQManager.StartQuestionnaire();
    }

    public static T[] GetBalancedLatinSquare<T>(T[] array, int participant) { 
        List<T> result = new List<T>();  

        // Based on "Bradley, J. V. Complete counterbalancing of immediate sequential effects in a Latin square design. J. Amer. Statist. Ass.,.1958, 53, 525-528. "
        for (int i = 0, j = 0, h = 0; i < array.Length; ++i) {
            var val = 0;
            if (i < 2 || i % 2 != 0) {
                val = j++;
            } else {
                val = array.Length - h - 1;
                ++h;
            }

            var idx = (val + participant) % array.Length;
            result.Add(array[idx]); 
        }

        if (array.Length % 2 != 0 && participant % 2 != 0) {
            result.Reverse(); 
        }

        return result.ToArray();
    }

    public static T[] GetLatinSquare<T>(T[] array, int participant) {
        // 1. Create initial square.
        int[,] latinSquare = new int[array.Length, array.Length];

        // 2. Initialise first row.
        latinSquare[0, 0] = 1;
        latinSquare[0, 1] = 2;

        for (int i = 2, j = 3, k = 0; i < array.Length; i++) {
            if (i % 2 == 1)
                latinSquare[0, i] = j++;
            else
                latinSquare[0, i] = array.Length - (k++);
        }

        // 3. Initialise first column.
        for (int i = 1; i <= array.Length; i++) {
            latinSquare[i - 1, 0] = i;
        }

        // 4. Fill in the rest of the square.
        for (int row = 1; row < array.Length; row++) {
            for (int col = 1; col < array.Length; col++) {
                latinSquare[row, col] = (latinSquare[row - 1, col] + 1) % array.Length;

                if (latinSquare[row, col] == 0)
                    latinSquare[row, col] = array.Length;
            }
        }

        T[] newArray = new T[array.Length];

        for (int col = 0; col < array.Length; col++) {
            int row = (participant + 1) % (array.Length);
            newArray[col] = array[latinSquare[row, col] - 1];
        }

        return newArray;
    }


    public static T[] GetAllPermutations<T>(T[] array, int participant) {
        List<List<T>> results = GeneratePermutations<T>(array.ToList());
        T[] newArray = new T[array.Length];
        int row = (participant + 1) % (results.Count);
        for (int i = 0; i < results[row].Count; i++) {
            newArray[i] = results[row][i];
        }
        return newArray;
    }

    private static List<List<T>> GeneratePermutations<T>(List<T> items) {
        T[] current_permutation = new T[items.Count];
        bool[] in_selection = new bool[items.Count];
        List<List<T>> results = new List<List<T>>();
        PermuteItems<T>(items, in_selection, current_permutation, results, 0);
        return results;
    }


    private static void PermuteItems<T>(List<T> items, bool[] in_selection, T[] current_permutation, List<List<T>> results, int next_position) {
        if (next_position == items.Count) {
            results.Add(current_permutation.ToList());
        } else {
            for (int i = 0; i < items.Count; i++) {
                if (!in_selection[i]) {
                    in_selection[i] = true;
                    current_permutation[next_position] = items[i];
                    PermuteItems<T>(items, in_selection, current_permutation, results, next_position + 1);
                    in_selection[i] = false;
                }
            }
        }
    }

    public void Shuffle<T>(T[] array) { 

        int n = array.Length;
        for (int i = 0; i < n; i++) {
            int r = i +
                (int)(Random.Range(0.0f, 1.0f) * (n - i));
            T t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }
}
