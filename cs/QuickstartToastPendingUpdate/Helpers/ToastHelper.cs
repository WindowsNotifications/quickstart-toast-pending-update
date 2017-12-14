using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace QuickstartToastPendingUpdate.Helpers
{
    public static class ToastHelper
    {
        public const string TAG_FOR_LUNCH = "lunch";

        public static void ShowOrderLunchToast()
        {
            ToastContent content = new ToastContent()
            {
                Launch = new QueryString()
                {
                    { "action", "viewLunchInfo" }
                }.ToString(),

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        HeroImage = new ToastGenericHeroImage()
                        {
                            Source = "https://picsum.photos/728/360?image=1080"
                        },

                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = "Would you like to order lunch today?"
                            }
                        }
                    }
                },

                Actions = new ToastActionsCustom()
                {
                    Buttons =
                    {
                        new ToastButton("Yes", new QueryString()
                        {
                            { "action", "orderLunch" }
                        }.ToString())
                        {
                            ActivationType = ToastActivationType.Background,
                            ActivationOptions = new ToastActivationOptions()
                            {
                                AfterActivationBehavior = ToastAfterActivationBehavior.PendingUpdate
                            }
                        },

                        new ToastButtonDismiss("No thanks")
                    }
                }
            };

            ShowToast(content);
        }

        public static void ShowWhichLunchToast()
        {
            ToastContent content = new ToastContent()
            {
                Launch = new QueryString()
                {
                    { "action", "viewWhichLunch" }
                }.ToString(),

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = "Which lunch would you like?"
                            }
                        }
                    }
                },

                Actions = new ToastActionsCustom()
                {
                    Inputs =
                    {
                        new ToastSelectionBox("lunchChoice")
                        {
                            Items =
                            {
                                new ToastSelectionBoxItem("burger", "BBQ Burger"),
                                new ToastSelectionBoxItem("sandwich", "Field Roast Sandwich (vegan)"),
                                new ToastSelectionBoxItem("fish", "Fish & Chips")
                            },

                            DefaultSelectionBoxItemId = "burger"
                        }
                    },

                    Buttons =
                    {
                        new ToastButton("Submit order", new QueryString()
                        {
                            { "action", "submitOrder" }
                        }.ToString())
                        {
                            ActivationType = ToastActivationType.Background,
                            ActivationOptions = new ToastActivationOptions()
                            {
                                AfterActivationBehavior = ToastAfterActivationBehavior.PendingUpdate
                            }
                        },

                        new ToastButtonDismiss("Cancel")
                    }
                },

                // We disable audio on all subsequent ones since they appear right after the user clicked something,
                // so the user's attention is already captured
                Audio = new ToastAudio() { Silent = true }
            };

            ShowToast(content);
        }

        public static void ShowSendingOrderToast()
        {
            ToastContent content = new ToastContent()
            {
                Launch = new QueryString()
                {
                    { "action", "viewSendingOrder" }
                }.ToString(),

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = "Hold on one second while we submit your order..."
                            },

                            new AdaptiveProgressBar()
                            {
                                Status = "Sending order...",
                                Value = AdaptiveProgressBarValue.Indeterminate
                            }
                        }
                    }
                },

                Audio = new ToastAudio() { Silent = true }
            };

            ShowToast(content);
        }

        public static void ShowOrderCompleteToast(string friendlyChoice)
        {
            ToastContent content = new ToastContent()
            {
                Launch = new QueryString()
                {
                    { "action", "viewOrder" }
                }.ToString(),

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = $"Your {friendlyChoice} is on the way!"
                            },

                            new AdaptiveText()
                            {
                                Text = "Your food should be delivered around 12:30 PM."
                            }
                        }
                    }
                }
            };

            ShowToast(content);
        }

        private static void ShowToast(ToastContent content)
        {
            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml())
            {
                Tag = TAG_FOR_LUNCH
            });
        }
    }
}
