require 'fileutils'

ROOT = Dir.pwd



INPUT_STATE = "#{ROOT}/input_state.ss"
OUTPUT_PUML_FPATH = "#{ROOT}/output.puml"
OUTPUT_CS_DIR = "#{ROOT}/unity_project/Assets/HelloFSM/Autogen";


task :default do
  Dir.chdir('NF.CLI.StateMachine') do
    Dir.chdir('NF.CLI.StateMachine.Exe') do
      sh 'dotnet restore'
      sh 'dotnet build'
      sh "dotnet run -- --input #{INPUT_STATE} --puml #{OUTPUT_PUML_FPATH} --cs #{OUTPUT_CS_DIR}"
    end
  end
end
