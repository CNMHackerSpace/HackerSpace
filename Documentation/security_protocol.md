# Security Checklist - Iteration 1

## 1. Transport and Communication
- [] Enable HTTPS using an SSL/TLS certificate
- [] Redirect all HTTP traffic to HTTPS
- [] Configure Cross-Origin Resource Sharing policies appropriately

## 2. Authentication and Authorization
- [] Use strong password requirements - i.e. length and complexity
- [] Store passwords using a secure hashing algorithm 
- [] Never store passwords in plain text
- [] Implement role-based access control 

## 3. Input and Data Handling
- [] Validate all user inputs on both client and server side
- [] Sanitize inputs to prevent SQL Injection attacks
- [] Sanitize inputs to prevent Cross-Site Scripting attacks

## 4. Secrets and Configuration
- [] Store API keys and secrets in environment variables, not in code
- [] Never commit sensitive credentials to GitHub
- [] Add a ' .gitignore' file to exclude sensitive config files
- [] Run a Trufflehog or equivalent to search for secrets

## 5. Dependencies and Updates
- [] Keep all frameworks and libraries up to date
- [] Regularly check for known vulnerabilities in dependencies

## 6. Error Handling and Logging
- [] Configure error messages to avoid exposing system details to users
- [] Log errors server-side for monitoring and debugging

## 7. Backups and Recovery
- [] Set up regular automated data backups
- [] Test that backups can be successfully restored