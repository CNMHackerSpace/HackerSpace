# Incident Response Plan
**Project:** Capstone Web Development Project  
**Audience:** Development Team  
**Last Updated:** March 2026
 
---
 
## Table of Contents
 
1. [Purpose & Scope](#1-purpose--scope)
2. [Roles & Responsibilities](#2-roles--responsibilities)
3. [Incident Severity Levels](#3-incident-severity-levels)
4. [General Incident Response Process](#4-general-incident-response-process)
5. [Incident: Exposed Secret or Credential](#5-incident-exposed-secret-or-credential)
6. [Incident: Security Vulnerability Discovered](#6-incident-security-vulnerability-discovered)
7. [Incident: Data Leak or Unauthorized Data Exposure](#7-incident-data-leak-or-unauthorized-data-exposure)
8. [Incident: Non-Compliance with Laws (GDPR)](#8-incident-non-compliance-with-laws-gdpr)
9. [Incident: Other / Miscellaneous Issues](#9-incident-other--miscellaneous-issues)
10. [Prevention Checklist](#10-prevention-checklist)
11. [Useful Tools & Resources](#11-useful-tools--resources)

 
## 1. Purpose & Scope
 
This plan defines how the team should **detect, respond to, and recover from** incidents during the development and operation of our capstone web project. It is intended to be a living document — update it as the project evolves.
 
**In scope:**
- All code repositories and branches
- All deployed environments (dev, staging, production)
- All team member accounts and credentials

 
## 2. Roles & Responsibilities
 
| Role | Responsibility |
|---|---|
| **Incident Lead** | Coordinates the response; rotates among senior team members |
| **Developer on Duty** | First to triage and assess the incident |
| **All Team Members** | Report suspected incidents immediately; do not stay silent |
| **Project Supervisor** | Notified for all Severity 1 incidents and GDPR breaches |
 
> **Rule #1:** If you find an issue, report it. Never try to quietly fix a security incident alone — transparency protects the whole team.

 
## 3. Incident Severity Levels
 
| Level | Description | Example | Response Time |
|---|---|---|---|
| **SEV-1 (Critical)** | Active harm, data exposed publicly, production down | Secret leaked to public repo, live data breach | Immediately |
| **SEV-2 (High)** | Potential for harm, not yet public | Vulnerability found in staging, GDPR concern | Within 2 hours |
| **SEV-3 (Medium)** | Limited impact, no user data at risk | Dependency with known CVE, minor misconfiguration | Within 24 hours |
| **SEV-4 (Low)** | Informational, best practice gap | Outdated package, weak password policy | Next sprint |

 
## 4. General Incident Response Process
 
All incidents follow this core loop, regardless of type:
 
```
DETECT → REPORT → CONTAIN → ASSESS → REMEDIATE → REVIEW
```
 
### Step-by-step:
 
1. **Detect** — A team member discovers an issue (via scanner, code review, alert, or accident).
2. **Report** — Immediately notify the team via your group chat. Open a GitHub Issue labeled `incident` with the severity tag.
3. **Contain** — Stop the bleeding. Revoke access, take down a page, revert a commit — whatever limits further damage.
4. **Assess** — Determine the full scope. What was affected? Who is impacted? For how long?
5. **Remediate** — Fix the root cause, not just the symptom.
6. **Review** — Hold a brief post-mortem. Document what happened and update this plan if needed.

 
## 5. Incident: Exposed Secret or Credential
 
**Examples:** API key committed to Git, database password in source code, `.env` file pushed to a public repo.
 
### How It Happens
- Accidentally committing a `.env` file
- Hardcoding credentials in source code
- Secret in a GitHub Actions log
 
### Immediate Actions (Contain First)
 
1. **Do NOT just delete the commit** — the secret is already in Git history and may be cached by GitHub.
2. **Revoke the secret immediately** — go to the relevant service (AWS, GitHub, Stripe, etc.) and rotate/invalidate the key right now.
3. **Assume the secret is compromised** — act as if it has already been used maliciously.
4. Notify the Incident Lead.
 
### Remediation Steps
 
1. Remove the secret from the codebase and all commit history using `git filter-repo` or BFG Repo Cleaner.
2. Force-push the cleaned history (coordinate with the team first).
3. Add the secret file (e.g., `.env`) to `.gitignore` immediately.
4. Issue a new credential and store it securely (see Prevention below).
5. Check access logs on the affected service for any unauthorized usage.
 
### Prevention
 
- **Never** commit `.env` files. Add them to `.gitignore` before the first commit.
- Use a secrets manager or environment variable injection (e.g., GitHub Secrets for CI/CD).
- Use a secret scanning tool like [GitGuardian](https://www.gitguardian.com/) or enable GitHub's built-in secret scanning.
- Set up pre-commit hooks using `detect-secrets` or `gitleaks` to block commits containing secrets.

 
## 6. Incident: Security Vulnerability Discovered
 
**Examples:** Dependency with a known CVE, SQL injection risk, XSS vulnerability, insecure authentication.
 
### Sources of Discovery
- `npm audit` or `pip audit` output
- Dependabot alerts on GitHub
- Manual code review
- Penetration testing
 
### Immediate Actions
 
1. Assess the severity using the CVE score (CVSS) if available.
2. Determine if the vulnerability is **exploitable in your environment**.
3. For SEV-1/SEV-2: Take the affected feature offline or add a temporary mitigation (e.g., WAF rule, feature flag).
4. Do not publicly disclose the vulnerability until it is fixed.
 
### Remediation Steps
 
1. **For dependency vulnerabilities:** Run `npm audit fix` or upgrade the affected package manually. Test thoroughly after upgrading.
2. **For code vulnerabilities (XSS, SQLi, etc.):** Fix the code, write a regression test to prevent recurrence, and do a focused code review of similar patterns elsewhere in the codebase.
3. Document the vulnerability, its fix, and any impact in the incident log.
 
### Prevention
 
- Run `npm audit` regularly and as part of your CI/CD pipeline.
- Enable Dependabot on your GitHub repo for automated alerts.
- Follow OWASP Top 10 guidelines during development.
- Perform code reviews with a security checklist.

 
## 7. Incident: Data Leak or Unauthorized Data Exposure
 
**Examples:** User data accidentally returned in an API response, database backup exposed publicly, logs containing personal information.
 
### Immediate Actions
 
1. **Contain immediately** — take the affected endpoint/page offline if needed.
2. Identify what data was exposed: what fields, how many users, for how long.
3. Preserve logs and evidence before making changes.
4. Notify the Incident Lead and Project Supervisor.
 
### Remediation Steps
 
1. Patch the code or configuration that caused the exposure.
2. Audit logs to determine the scope of access.
3. If real user data was involved → escalate to GDPR process (see Section 8).
4. Notify affected users if required (see GDPR section for thresholds).
5. Review all other API endpoints and data-return logic for similar issues.
 
### Prevention
 
- Always apply the **principle of least privilege** — only return the data fields a client actually needs.
- Never log sensitive fields (passwords, tokens, PII).
- Use test/mock data in development — avoid using real user data in non-production environments.
- Implement API response filtering and output validation.

 
## 8. Incident: Non-Compliance with Laws (GDPR)
 
**Applicable when** the site collects, stores, or processes personal data of users in the EU/UK (names, emails, IP addresses, cookies, etc.).
 
### Key GDPR Obligations to Know
 
| Obligation | What It Means for Your Project |
|---|---|
| **Lawful basis** | You need a reason to collect data (e.g., user consent) |
| **Privacy notice** | Users must be told what data you collect and why |
| **Data minimization** | Only collect what you actually need |
| **Right to erasure** | Users can request their data be deleted |
| **Breach notification** | Notify the supervisory authority within **72 hours** of becoming aware of a breach |
 
### Incident: Personal Data Breach
 
A personal data breach is any accidental or unlawful destruction, loss, alteration, or unauthorized disclosure of personal data.
 
**Immediate Actions:**
 
1. Contain the breach (take affected systems offline if needed).
2. Document the discovery time — **the 72-hour clock starts now**.
3. Notify the Incident Lead and Project Supervisor immediately.
 
**Assessment Questions:**
 
- What categories of personal data were involved (names, emails, passwords)?
- How many individuals are affected?
- What is the likely consequence for those individuals?
- Has the data left your control (i.e., was it accessed externally)?
 
**Remediation Steps:**
 
1. Fix the root cause.
2. If the breach is likely to result in a risk to individuals' rights: **notify your supervisory authority within 72 hours** (in the UK: ICO; in the EU: your country's DPA).
3. If the breach poses a **high risk** to individuals: notify the affected users directly without undue delay.
4. Document everything: what happened, what data, when discovered, what actions were taken.
 
**Incident: Consent or Privacy Policy Non-Compliance**
 
- Immediately stop the non-compliant data collection.
- Review and update your privacy policy / cookie consent banner.
- Assess whether any data collected without proper consent needs to be deleted.
 
### Prevention
 
- Only collect personal data with explicit user consent (opt-in).
- Maintain a clear, plain-language privacy policy.
- Use a cookie consent banner if using analytics or tracking.
- Store passwords as hashed values — never plaintext.
- Conduct a basic Data Protection Impact Assessment (DPIA) before launching features that handle personal data.
 
 
## 9. Incident: Other / Miscellaneous Issues
 
### Dependency License Violation
**If a library has an incompatible license (e.g., GPL in a proprietary product):**
1. Stop using the dependency in production immediately.
2. Find an alternative library with a compatible license (MIT, Apache 2.0, BSD are generally safe).
3. Review all other dependencies using a tool like `license-checker`.
 
### Unauthorized Access to Systems
**If a team member's account is compromised:**
1. Revoke all tokens and sessions for that account immediately.
2. Reset credentials and enable/enforce MFA.
3. Review audit logs for any actions taken using the compromised account.
4. Rotate any secrets the account had access to.
 
### Accidental Deletion of Data or Code
1. Check for backups or Git history.
2. Restore from the most recent clean backup.
3. Document what was lost and for how long.
4. Implement automated backups if not already in place.
 

## 10. Prevention Checklist
 
Use this checklist at the start of the project and before each major release:
 
- [ ] `.env` and config files are in `.gitignore`
- [ ] No secrets or credentials are hardcoded in source code
- [ ] Secret scanning is enabled on the repository
- [ ] `npm audit` passes with no critical vulnerabilities
- [ ] Dependabot or equivalent is configured
- [ ] All user passwords are stored as hashed values (bcrypt, argon2, etc.)
- [ ] HTTPS is enforced in all environments
- [ ] A privacy policy is in place (if collecting user data)
- [ ] Cookie consent is implemented (if using cookies/tracking)
- [ ] Principle of least privilege applied to database and API access
- [ ] Logging does not include sensitive personal data
- [ ] Team members have individual accounts (no shared credentials)
- [ ] MFA is enabled on all developer accounts (GitHub, cloud providers, etc.)

 
## 11. Useful Tools & Resources
 
| Tool / Resource | Purpose |
|---|---|
| [GitHub Secret Scanning](https://docs.github.com/en/code-security/secret-scanning) | Detects secrets committed to repos |
| [GitGuardian](https://www.gitguardian.com/) | Real-time secret detection |
| [gitleaks](https://github.com/gitleaks/gitleaks) | Pre-commit secret scanning |
| `npm audit` | Identify vulnerable npm packages |
| [Dependabot](https://github.com/dependabot) | Automated dependency updates |
| [OWASP Top 10](https://owasp.org/www-project-top-ten/) | Common web security risks |
| [Have I Been Pwned](https://haveibeenpwned.com/) | Check if credentials are in known breaches |
| [ICO Breach Report (UK)](https://ico.org.uk/for-organisations/report-a-breach/) | GDPR breach reporting (UK) |
| [GDPR Art. 33](https://gdpr-info.eu/art-33-gdpr/) | Breach notification requirements |
| [BFG Repo Cleaner](https://rtyley.github.io/bfg-repo-cleaner/) | Remove secrets from Git history |

 
## Incident Log Template
 
Use this template to record every incident in your project wiki or a `INCIDENTS.md` file:
 
```
## Incident #[N] — [Short Title]
 
- **Date Discovered:** 
- **Discovered By:** 
- **Severity:** SEV-[1/2/3/4]
- **Type:** [Secret / Vulnerability / Data Leak / Compliance / Other]
- **Summary:** 
- **Impact:** 
- **Timeline:**
  - [time] — Discovered
  - [time] — Contained
  - [time] — Remediated
- **Root Cause:** 
- **Actions Taken:** 
- **Prevention Going Forward:** 
```
 
---
 
*This document should be reviewed and updated at the start of each sprint or when the project scope changes.*