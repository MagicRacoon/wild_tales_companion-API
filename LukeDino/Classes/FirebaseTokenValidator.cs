using FirebaseAdmin.Auth;

namespace LukeDino.Classes
{
    public class FirebaseTokenValidator
    {
        public async Task<FirebaseToken?> ValidateAsync(string idToken)
        {
            try
            {
                return await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
            }
            catch
            {
                return null;
            }
        }
    }
}
