using System.Collections.Generic;
using System.Threading.Tasks;
using CSCommonSecrets;

public sealed class PaymentCardSimplified
{
    // Non visible
    public int zeroBasedIndexNumber { get; set; }

    // Visible elements

    public bool IsSecure { get; set; }

    public string Title { get; set; }

    public string NameOnTheCard { get; set; }

    public string CardType { get; set; }

    public string Number { get; set; }

    public string SecurityCode { get; set; }

    public string CreationTime { get; set; }

    public string ModificationTime { get; set; }

    private static readonly string dateTimeOffsetToStringConvert = "u";

    public static async Task<PaymentCardSimplified[]> CreatePaymentCardSimplifieds(List<PaymentCard> paymentCards, List<PaymentCardSecret> paymentCardSecrets, ISecurityAsyncFunctions securityFunctions, IReadDerivedPassword derivedPasswords)
    {
        List<PaymentCardSimplified> returnValues = new List<PaymentCardSimplified>();

        for (int i = 0; i < paymentCards.Count; i++)
        {
            PaymentCardSimplified paymentCardSimplified = new PaymentCardSimplified()
            {
                zeroBasedIndexNumber = i,
                IsSecure = false,
                Title = paymentCards[i].GetTitle(),
                NameOnTheCard = paymentCards[i].GetNameOnCard(),
                CardType = paymentCards[i].GetCardType(),
                Number = paymentCards[i].GetNumber(),
                SecurityCode = paymentCards[i].GetSecurityCode(),
                CreationTime = paymentCards[i].GetCreationTime().ToString(dateTimeOffsetToStringConvert),
                ModificationTime = paymentCards[i].GetModificationTime().ToString(dateTimeOffsetToStringConvert),
            };

            returnValues.Add(paymentCardSimplified);
        }

        for (int i = 0; i < paymentCardSecrets.Count; i++)
        {
            string keyIdentifier = paymentCardSecrets[i].GetKeyIdentifier();
            if (derivedPasswords.DoesDerivedPasswordExist(keyIdentifier))
            {
                byte[] derivedPassword = derivedPasswords.GetDerivedPassword(keyIdentifier);
                PaymentCardSimplified paymentCardSimplified = new PaymentCardSimplified()
                {
                    zeroBasedIndexNumber = i,
                    IsSecure = true,
                    Title = await paymentCardSecrets[i].GetTitleAsync(derivedPassword, securityFunctions),
                    NameOnTheCard = await paymentCardSecrets[i].GetNameOnCardAsync(derivedPassword, securityFunctions),
                    CardType = await paymentCardSecrets[i].GetCardTypeAsync(derivedPassword, securityFunctions),
                    Number = await paymentCardSecrets[i].GetNumberAsync(derivedPassword, securityFunctions),
                    SecurityCode = await paymentCardSecrets[i].GetSecurityCodeAsync(derivedPassword, securityFunctions),
                    CreationTime = (await paymentCardSecrets[i].GetCreationTimeAsync(derivedPassword, securityFunctions)).ToString(dateTimeOffsetToStringConvert),
                    ModificationTime = (await paymentCardSecrets[i].GetModificationTimeAsync(derivedPassword, securityFunctions)).ToString(dateTimeOffsetToStringConvert),
                };

                returnValues.Add(paymentCardSimplified);
            }
        }

        return returnValues.ToArray();
    }
}