pipeline {
    agent any

    environment {
        IIS_SHARE = '\\\\iis-2019.wmi.amu.edu.pl\\s473603\\public_iis\\'
        OFFLINE_FILE = "${IIS_SHARE}app_offline.htm"
    }

    stages {
        stage('Prepare for Deployment') {
            steps {
                echo 'Creating app_offline.htm to disable application temporarily...'
                writeFile file: OFFLINE_FILE, text: 'Application offline for update.'
            }
        }

        stage('Build') {
            steps {
                echo 'Building the application...'
                sh 'dotnet restore'
                sh 'dotnet build --configuration Release'
            }
        }

        stage('Publish') {
            steps {
                echo 'Publishing the application...'
                sh 'dotnet publish --configuration Release --output ./publish'
            }
        }

        stage('Deploy') {
            steps {
                echo 'Deploying to network share...'
                script {
                    bat """
                    xcopy /E /Y /I ./publish ${IIS_SHARE}
                    """
                }
            }
        }

        stage('Cleanup') {
            steps {
                echo 'Removing app_offline.htm to bring application back online...'
                script {
                    if (fileExists(OFFLINE_FILE)) {
                        bat "del ${OFFLINE_FILE}"
                    }
                }
            }
        }
    }
}
