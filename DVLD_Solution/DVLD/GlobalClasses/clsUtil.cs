using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.GlobalClasses
{


    public class clsUtil
    {
        public static void ShowThisFeatureIsNotReady()
        {
             MessageBox.Show("This Feature Is Not Ready", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static DialogResult ShowError(string message)
        {
            return MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static DialogResult ShowSuccessful(string message)
        {
            return MessageBox.Show(message, "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static DialogResult ShowConfirm(string message)
        {
            return MessageBox.Show(message,"Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
        }
        public static bool CreateFolderIfDoesNotExist(string folderName)
        {
            if (!Directory.Exists(folderName))
            {
                try
                {
                    Directory.CreateDirectory(folderName);
                    return true;

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
            }

            return true;
        }

        public static string GenerateGUID()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }
        public static string ReplaceFileNameWithGUID(string sourceFile)
        {
            string destinationFile = "";
            FileInfo sourceFileInfo = new FileInfo(sourceFile);
            destinationFile = GenerateGUID() + sourceFileInfo.Extension;
            return destinationFile;
            
        }
        public static bool CopyImageToProjectImagesFolder(ref string sourceFile)
        {

            string destinationDirectory = "E:\\Learn\\ProgrammingAdvices\\14\\DVLD\\People_Images\\";

            if(!CreateFolderIfDoesNotExist( destinationDirectory))
            {
                return false;
            }

            string destinationFile = destinationDirectory + ReplaceFileNameWithGUID(sourceFile);

            try
            {
                File.Copy(sourceFile, destinationFile, true);
            }
            catch(IOException e)
            {
                MessageBox.Show(e.Message,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            sourceFile = destinationFile;
            return true;

        }

        public static bool DeleteImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return true;

            try
            {
                File.Delete(fileName);
            }
            catch(IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}
