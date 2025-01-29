using System;
using System.Linq;
using System.Reflection;
using BP.Service;
using TMPro;
using UnityEngine;

namespace BP.Console
{
    public class DevConsole : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputText;
        [SerializeField] private TextMeshProUGUI outputTextPrefab;
        [SerializeField] private Transform outputContainer;
        
        private ConsoleCommand _command;

        private void Start()
        {
            _command = new ConsoleCommand(this);
            ServiceManager.Add(new DevConsoleService(this));
        }

        private void OnDestroy()
        {
            ServiceManager.Remove<DevConsoleService>();
        }

        private void OnEnable()
        {
            inputText.onSubmit.AddListener(ExecuteCommand);
        }

        private void OnDisable()
        {
            inputText.onSubmit.RemoveListener(ExecuteCommand);
        }

        public void Log(string msg)
        {
            var text = Instantiate(outputTextPrefab, outputContainer);
            text.SetText(msg);
        }

        public void LogError(string msg) => Log($"<color=red>{msg}</color>");
        public void LogSuccess(string msg) => Log($"<color=green>{msg}</color>");

        private void ExecuteCommand(string commandstr)
        {
            string[] commands = commandstr.Split(' ').Select(c => c.Trim()).ToArray();
            var args = commands.ToList();
            args.RemoveAt(0);
            
            var method = typeof(ConsoleCommand).GetMethod(commands[0],
                BindingFlags.Default | BindingFlags.Instance | BindingFlags.Public);

            if (method != null)
            {
                var parameters = method.GetParameters();
                if (args.Count() != parameters.Length)
                {
                    LogError($"Invalid number of arguments for command: {commands[0]}");
                    return;
                }
                
                object[] parameterValues = new object[parameters.Length];
                for (int i = 0; i < parameters.Length; i++)
                {
                    try
                    {
                        parameterValues[i] = Convert.ChangeType(args[i], parameters[i].ParameterType);
                    }
                    catch (Exception ex)
                    {
                        parameterValues[i] = args[i];
                    }
                }

                try
                {
                    method.Invoke(_command, parameterValues);
                }
                catch (Exception e)
                {
                    LogError(e.Message);
                }
                
                inputText.text = "";
            }
            else
            {
                LogError($"Command {commands[0]} not found");
            }
        }
    }
}
