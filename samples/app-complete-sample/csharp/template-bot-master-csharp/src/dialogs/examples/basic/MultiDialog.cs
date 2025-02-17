﻿using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Teams.TemplateBotCSharp.Properties;
using Microsoft.Teams.TemplateBotCSharp.src.dialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Teams.TemplateBotCSharp.Dialogs
{
    public class MultiDialog1 : ComponentDialog
    {
        public MultiDialog1() : base(nameof(MultiDialog1))
        {
            InitialDialogId = nameof(WaterfallDialog);
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                BeginMultiDialog1Async,
            }));
        }

        private async Task<DialogTurnResult> BeginMultiDialog1Async(
            WaterfallStepContext stepContext,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await stepContext.Context.SendActivityAsync(Strings.HelpCaptionMultiDialog1);

            return await stepContext.EndDialogAsync(null, cancellationToken);
        }
    }

    public class MultiDialog2 : ComponentDialog
    {
        protected readonly IStatePropertyAccessor<RootDialogState> _conversationState;
        public MultiDialog2(IStatePropertyAccessor<RootDialogState> conversationState) : base(nameof(MultiDialog2))
        {
            this._conversationState = conversationState;
            InitialDialogId = nameof(WaterfallDialog);
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                BeginMultiDialog2Async,
            }));
        }

        private async Task<DialogTurnResult> BeginMultiDialog2Async(
WaterfallStepContext stepContext,
CancellationToken cancellationToken = default(CancellationToken))
        {
            var message = CreateMultiDialog(stepContext);

            //Set the Last Dialog in Conversation Data
            var currentState = await this._conversationState.GetAsync(stepContext.Context, () => new RootDialogState());
            currentState.LastDialogKey = Strings.LastDialogMultiDialog2;
            await this._conversationState.SetAsync(stepContext.Context, currentState);

            await stepContext.Context.SendActivityAsync(message);
            return await stepContext.EndDialogAsync(null, cancellationToken);
        }

        private IMessageActivity CreateMultiDialog(WaterfallStepContext context)
        {
            var message = context.Context.Activity;
            if (message.Attachments != null)
            {
                message.Attachments = null;
            }

            if (message.Entities.Count >= 1)
            {
                message.Entities.Remove(message.Entities[0]);
            }
            var attachment = CreateMultiDialogCard();
            message.Attachments = new List<Attachment>() { attachment };
            return message;
        }

        private Attachment CreateMultiDialogCard()
        {
            return new HeroCard
            {
                Title = Strings.MultiDialogCardTitle,
                Subtitle = Strings.MultiDialogCardSubTitle,
                Text = Strings.MultiDialogCardText,
                Images = new List<CardImage> { new CardImage(ConfigurationManager.AppSettings["BaseUri"].ToString() + "/public/assets/computer_person.jpg") },
                Buttons = new List<CardAction>
                {
                   new CardAction(ActionTypes.ImBack, Strings.CaptionInvokeHelloDailog, value: Strings.cmdHelloDialog),
                   new CardAction(ActionTypes.ImBack, Strings.CaptionInvokeMultiDailog, value: Strings.cmdMultiDialog1),
                }
            }.ToAttachment();
        }
    }
}