using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataManager : MonoBehaviour
{

    static bool isLoaded = false;

    void Awake()
    {
        if (!isLoaded)
        {
            // load
            StoryDatas = SerializationManager.LoadJSON<List<StoryData>>("storyData");
            StoryPlayerDatas = SerializationManager.LoadJSON<List<StoryPlayerData>>("storyPlayerData");
            RearrangementDatas = SerializationManager.LoadJSON<List<RearrangementData>>("rearrangementData").
            SelectMany(rd => rd.indices, (rd, rdIndex) => new {rdIndex, rd}).ToDictionary(rd => rd.rdIndex, rd => rd.rd);

            /*
            StoryDatas.Add(new StoryData
            {
                index = 0,
                childrenIndices = new List<int>(),
                character = StoryData.Character.MrsJacobs,

                title = "Time is right",
                initialText = new List<string> ()
                {
                    "Cold night.",
                    "Desolate."
                },
                outcomes = new StoryData.OutcomeList()
                {
                    new StoryData.Outcome()
                    {
                        outcomeIndex = 0,
                        outcomeConditions = new List<OutcomeCondition>() {OutcomeCondition.FromString("seq 0.1, 0.2, 0.3")},
                        outcomeText = new List<string>()
                        {
                            "Fire.",
                            "Rain."
                        },
                        enabledStories = new List<int>(),
                        enabledOutcomes = new List<StoryData.Outcome.OutcomeIndices>()
                    }
                },
                lastLineTypes = new Dictionary<int, StoryData.LineFlags>()
                {
                    {-1, StoryData.LineFlags.Draggable},
                    {-2, StoryData.LineFlags.None}
                },
                lineEffects = new Dictionary<int, StoryData.Effect>()
            });

            StoryPlayerDatas.Add(new StoryPlayerData()
            {
                index = 0,
                isDiscovered = true,
                isRead = false,
                outcomeDiscovered = new List<bool>()
                {
                    true, false, false
                },
                selectedOutcome = 0
            });

            RearrangementDatas.Add(new RearrangementData()
            {
                indices = new int[] {0},
                rearrangementTextboxIndices = new Dictionary<int, List<RearrangementData.TextboxIndices>>()
                {
                    {0, new List<RearrangementData.TextboxIndices>()
                    {
                        new RearrangementData.TextboxIndices() {storyIndex = 0, textboxIndex = 0},
                        new RearrangementData.TextboxIndices() {storyIndex = 0, textboxIndex = 1}
                    }}
                }
            });
            */

            // save
            SerializationManager.SaveJSON("storyData", StoryDatas);
            SerializationManager.SaveJSON("storyPlayerData", StoryPlayerDatas);
            SerializationManager.SaveJSON("rearrangementData", RearrangementDatas.Values.Distinct().OrderBy(d => d.indices[0]).ToList());

            // backup
            //SerializationManager.Backup("storyData", storyDatas.storyDatas);
            isLoaded = true;
        }
    }

    // for main game
    public static List<StoryData> StoryDatas = new List<StoryData>();
    public static List<StoryPlayerData> StoryPlayerDatas = new List<StoryPlayerData>();
    public static Dictionary<int, RearrangementData> RearrangementDatas = new Dictionary<int, RearrangementData>();

    // for animated and rearrangement
    public static int[] SelectedStoryIndices;
    public static int SelectedIndex;

    public static List<Vector2> StoryPosition = new List<Vector2>();

    public static List<StoryData.Outcome.OutcomeIndices> AnimatedOutcomes = new List<StoryData.Outcome.OutcomeIndices>();
}