namespace MVP.Api
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using MVP.Api.Models;

    public partial class ApiClient
    {
        private const string AwardConsiderationEndpoint = "awardconsideration";

        /// <summary>
        /// Gets the answers provided for the current award questions.
        /// </summary>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>A collection of <see cref="AwardQuestionAnswer"/> objects.</returns>
        public async Task<IEnumerable<AwardQuestionAnswer>> GetAwardQuestionAnswersAsync(
            CancellationToken cancellationToken = default)
        {
            return await this.GetAsync<IEnumerable<AwardQuestionAnswer>>(
                $"{AwardConsiderationEndpoint}/GetAnswers",
                true,
                null,
                cancellationToken);
        }

        /// <summary>
        /// Gets the current award questions.
        /// </summary>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>A collection of <see cref="AwardQuestion"/> objects.</returns>
        public async Task<IEnumerable<AwardQuestion>> GetCurrentAwardQuestionsAsync(
            CancellationToken cancellationToken = default)
        {
            return await this.GetAsync<IEnumerable<AwardQuestion>>(
                $"{AwardConsiderationEndpoint}/getcurrentquestions",
                true,
                null,
                cancellationToken);
        }

        /// <summary>
        /// Saves the answers for the current award questions in an editable state to be submitted at a later date.
        /// </summary>
        /// <param name="answers">The answers to save for editing later.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>True if the answers saved successfully; otherwise, false.</returns>
        public async Task<bool> SaveAwardQuestionAnswersAsync(
            IEnumerable<AwardQuestionAnswer> answers,
            CancellationToken cancellationToken = default)
        {
            return await this.PostAsync(
                $"{AwardConsiderationEndpoint}/saveanswers",
                answers,
                true,
                null,
                cancellationToken);
        }

        /// <summary>
        /// Submits and finalizes the current answers for award consideration questions.
        /// <para>
        /// Once submitted answers can't be changed anymore under current renewal cycle.
        /// </para>
        /// </summary>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>True if the answers saved successfully; otherwise, false.</returns>
        public async Task<bool> SubmitAwardQuestionAnswersAsync(
            CancellationToken cancellationToken = default)
        {
            return await this.PostAsync(
                $"{AwardConsiderationEndpoint}/SubmitAnswers",
                null,
                true,
                null,
                cancellationToken);
        }
    }
}