using Firebase.Storage;
using Microsoft.AspNetCore.Components.Forms;

namespace DotNETProject.Client.Service
{
    public class FirebaseService
    {
        private readonly long MAXALLOWEDSIZE = 2147483648;
        private readonly string firebaseUrl = "dotnetproject-339c8.appspot.com";
        public FirebaseService()
        {

        }
        public async Task<string> HandleFirebaseUpload(IBrowserFile file, string folderPath)
        {
            try
            {

                var storage = new FirebaseStorage(firebaseUrl);

                folderPath = folderPath.Trim('/');

                var folderRef = storage.Child(folderPath);
                var folderExists = await FolderExistsAsync(folderRef);
                if (!folderExists)
                {
                    await CreateFolderAsync(folderRef);
                }

                string imageFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.Name)}";

                var imageUrl = await storage.Child(folderPath)
                    .Child(imageFileName)
                    .PutAsync(file.OpenReadStream(maxAllowedSize: MAXALLOWEDSIZE));

                return imageUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
        private async Task<bool> FolderExistsAsync(FirebaseStorageReference folderRef)
        {
            try
            {
                await folderRef.GetDownloadUrlAsync();
                return true;
            }
            catch (FirebaseStorageException)
            {
                return false;
            }
        }
        private async Task CreateFolderAsync(FirebaseStorageReference folderRef)
        {
            try
            {
                await folderRef.Child(".placeholder").PutAsync(new MemoryStream());
            }
            catch (FirebaseStorageException ex)
            {
                Console.WriteLine($"Error creating folder: {ex.Message}");
            }
        }
    }
}
