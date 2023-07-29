using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ftpsolution
{
    class Program
    {
        static void Main(string[] args)
        {
            string localFilePath = "/Users/rs/Documents/ISO815-Practica7-main/prueba3.txt";
            string remoteFilePath = "/home/ftpuser/";
            string ftpHost = "129.80.203.120";
            string ftpUser = "ftpuser";
            string ftpPass = "powky";
            int ftpPort = 21;

            UploadFileViaLftp(localFilePath, remoteFilePath, ftpHost, ftpUser, ftpPass, ftpPort);

            //Console.WriteLine("Presiona Enter para salir...");
            //Console.ReadLine();
        }

        static void UploadFileViaLftp(string localFilePath, string remoteFilePath, string ftpHost, string ftpUser, string ftpPass, int ftpPort = 21)
        {
            // Construir el comando para el FTP
            string lftpCommand = $"-u {ftpUser},{ftpPass} -e \"put {localFilePath} -o {remoteFilePath}; quit\" -p {ftpPort} {ftpHost}";

            // Crear una nueva instancia del proceso
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "lftp",
                    Arguments = lftpCommand,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            // Iniciar el proceso
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();
            int exitCode = process.ExitCode;

            // Cerrar el proceso
            process.Close();

            // Verificar si hubo errores y mostrar el resultado
            if (exitCode == 0)
            {
                Console.WriteLine("Archivo cargado correctamente.");
            }
            else
            {
                Console.WriteLine($"Error al cargar: {error}");
            }
        }
    }
}
