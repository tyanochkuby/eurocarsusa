pipeline {
    agent any

    environment {
        IIS_SHARE = '\\\\iis-2019.wmi.amu.edu.pl\\s473603\\public_iis\\'
        OFFLINE_FILE = "${IIS_SHARE}app_offline.htm"
    }

    stages {
        stage('Prepare for Deployment') {
            steps {
                script {
                    withCredentials([usernamePassword(credentialsId: 'network-share-credentials', usernameVariable: 'USER', passwordVariable: 'PASS')]) {
                        // Map network drive using the retrieved credentials
                        bat "NET USE Z: ${IIS_SHARE} /user:%USER% %PASS%"
                        
                        // Ensure Z: is mapped correctly and create app_offline.htm
                        writeFile file: 'Z:\\app_offline.htm', text: 'Application offline for maintenance'
                    }
                }
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
                deleteFile 'Z:\\app_offline.htm'
                bat "NET USE Z: /DELETE"
            }
        }
    }
}
