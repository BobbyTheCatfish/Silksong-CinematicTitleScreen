using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CinematicTitleScreen.Data
{
    internal static class IntroSceneData
    {
        public static int SceneIndexOverride = 0;

        static readonly List<SceneInfo> MossScenes = [
            new SceneInfo {
                Name = "Tut_01",
                RandomOrder = false,
                Animations = [
                    new Animation {
                        StartDelay = 25,
                        EndDelay = 10,
                        Speed = 0.75f,
                        Positions = [
                            new Vector2(49.04f, 10.21f),
                            new Vector2(49.04f, 107.65f)
                        ]
                    },
                    new Animation {
                        StartDelay = 0,
                        EndDelay = 0,
                        Speed = 1f,
                        Positions = [
                            new Vector3(17.07f, 35.33f, 0),
                            new Vector3(33.02f, 35.33f, 2),
                            new Vector3(37.02f, 35.33f, 1),
                            new Vector3(50.96f, 38.38f, 1),
                            new Vector3(54.96f, 38.38f, 2),
                            new Vector3(62.87f, 38.38f, 0)
                        ],
                        AllowReverse = true
                    }
                ],
                DisablePaths = ["left3/haze2 (3)"]
            },

            new SceneInfo {
                Name = "Bonetown",
                Animations = [
                    Animation.SingleFrame(new Vector2(242.75f, 12.62f)),
                    Animation.SingleFrame(new Vector2(288.38f, 26.75f)),
                    new Animation {
                        StartDelay = 1,
                        EndDelay = 25,
                        Speed = 1,
                        Positions = [
                            new Vector2(259.37f, 64.93f),
                            new Vector2(209.34f, 64.93f)
                        ],
                        AllowReverse = true
                    }
                ],
                EnablePaths = [
                    "Mapper Control/Shakra Resting"
                ]
            },

            new SceneInfo {
                Name = "Bonegrave",
                Animations = [
                    Animation.SingleFrame(new Vector2(197.88f, 12.84f), useLight: false),
                    Animation.SingleFrame(new Vector2(252.97f, 38.03f)),
                    Animation.SingleFrame(new Vector2(253.54f, 13.3f)),
                    Animation.SingleFrame(new Vector2(292.88f, 70.15f))
                ],
                RandomOrder = false,
                DisablePaths = [
                    "Col_Glow_Remasker"
                ]
            },

            new SceneInfo {
                Name = "Bone_11",
                Animations = [
                    Animation.SingleFrame(new Vector2(82.83f, 9.30f)),
                    Animation.SingleFrame(new Vector2(85.85f, 22.64f)),
                    Animation.SingleFrame(new Vector2(21.42f, 9.52f))
                ],
                DisablePaths = [
                    "Silk Possession Obj(Clone)"
                ]
            },

            new SceneInfo {
                Name = "Mosstown_02c",
                Animations = [
                    new Animation {
                        Speed = 1,
                        StartDelay = 25,
                        EndDelay = 25,
                        AllowReverse = true,
                        UseLight= false,
                        Positions = [
                            new Vector2(16.18f, 7.7f),
                            new Vector2(40.65f, 7.7f)
                        ],
                    }
                ]
            },
        ];

        static readonly List<SceneInfo> WeaverScenes = [
            new SceneInfo {
                Name = "Weave_02",
                Animations = [
                    new Animation {
                        Speed = 50,
                        StartDelay = 25,
                        EndDelay = 25,
                        AllowReverse = true,
                        Positions = [
                            new Vector2(22.59f, 112.31f),
                            new Vector2(22.59f, 183.48f)
                        ]
                    }
                ],
                PersistantBools = [
                    BoolData("weaver_lift_power_chamber", "Weave_12")
                ]
            },

            new SceneInfo {
                Name = "Weave_10",
                Animations = [
                    Animation.SingleFrame(new Vector2(79.1f, 15.54f), useLight: false)
                ],
                DisablePaths = [
                    "Crest Upgrade Shrine/crest_shrine_break_tube parent/crest_shrine_break_tube/Mask",
                    "Crest Upgrade Shrine/crest_shrine_break_tube parent/crest_shrine_break_tube (1)/Mask",
                    "Crest Upgrade Shrine/crest_shrine_break_tube parent/crest_shrine_break_tube (2)/Mask"
                ]
            },

            new SceneInfo {
                Name = "Weave_07",
                Animations = [
                    Animation.SingleFrame(new Vector2(26.11f, 110.63f))
                ]
            },

            new SceneInfo {
                Name = "Weave_08",
                Animations = [
                    new Animation {
                        AllowReverse = true,
                        Positions = [
                            new Vector2(46.63f, 35.27f),
                            new Vector2(46.63f, 58.27f)
                        ]
                    }
                ],
                AddHero = true
            },

            new SceneInfo {
                Name = "Bone_East_Weavehome",
                Animations = [
                    new Animation {
                        Positions = [
                            new Vector2(22.71f, 97.71f),
                            new Vector3(36.05f, 97.71f, 2),
                            new Vector3(46.57f, 97.71f, 1),
                            new Vector3(57.09f, 95.59f, 1),
                            new Vector3(67.61f, 95.59f, 2),
                            new Vector2(116.4f, 95.59f),
                        ]
                    }
                ],
                AddHero = true,
            },

            new SceneInfo {
                Name = "Shadow_Weavehome",
                Animations = [
                    Animation.SingleFrame(new Vector2(127.27f, 58.62f), useLight : false),
                    new Animation {
                        Positions = [
                            new Vector2(15.82f, 58.3f),
                            new Vector3(28.85f, 58.3f, 2),
                            new Vector3(32.9f, 58.3f, 2),
                            new Vector3(36.95f, 47.69f, 1),
                            new Vector3(41, 41.88f, 2),
                            new Vector3(47.05f, 41.88f, 2),
                            new Vector2(90.04f, 41.88f)
                        ]
                    },
                    Animation.SingleFrame(new Vector2(80.9f, 58.62f), useLight : false),
                ],
                PersistantBools = [
                    BoolData("weaver_lift_power_chamber", "Shadow_Weavehome")
                ],
                AddHero = true,
                RandomOrder = false
            },

            new SceneInfo {
                Name = "Crawl_05",
                AddHero = true,
                Animations = [
                    new Animation {
                        AllowReverse = true,
                        Positions = [
                            new Vector2(83.65f, 22.32f),
                            new Vector2(23.08f, 22.32f)
                        ]
                    },
                    Animation.SingleFrame(new Vector2(206.07f, 19.9f))
                ]
            }
        ];

        static readonly List<SceneInfo> BoneScenes = [
            new SceneInfo {
                Name = "Bellshrine",
                Animations = [
                    Animation.SingleFrame(new Vector2(20.45f, 10.15f))
                ],
                EnablePaths = [
                    "Whole Scene/Bell Bench"
                ],
                DisablePaths = [
                    "Whole Scene/Bellshrine Sequence/Bell Shrine Lever",
                    "Whole Scene/bellshrine_gate_curved"
                ]
            },

            new SceneInfo {
                Name = "Bone_05",
                Animations = [
                    Animation.SingleFrame(new Vector2(83.25f, 8.65f))
                ]
            }
        ];

        public static readonly List<SceneInfo> Scenes = [
            ..MossScenes,
            ..WeaverScenes,
            ..BoneScenes
        ];

        public static SceneInfo GetRandomScene()
        {
            if (SceneIndexOverride != -1)
            {
                return Scenes[SceneIndexOverride];
            }

            return Scenes.GetRandomElement();
        }

        static PersistentItemData<bool> BoolData(string key, string scene)
        {
            return new PersistentItemData<bool>
            {
                ID = key,
                SceneName = scene,
                IsSemiPersistent = true,
                Value = true,
                Mutator = SceneData.PersistentMutatorTypes.None
            };
        }
    }
}
