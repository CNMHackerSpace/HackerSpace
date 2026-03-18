# Secret Scanning Tools Comparison - Iteration 2

A comparison of tools available to detect accidentally committed secrets in our repository.

### TruffleHog
**What it does**
TruffleHog scans our repository's entire Git history - not just the current code - looking for high-entropy strings and known secret patterns. This means it can find secrets even if they were deleted in a later commit. 

**Strengths**
- Scans full Git history, not just current files
- Detects over 700 credential types (AWS, GitHub tokens, Stripe keys, etc.)
- Can be run locally or integrated into a CI/CD pipeline
- Free and open source
- Can scan GitHub repos directly without cloning

**Weaknesses:**
- Command-line tool — no graphical interface
- Can produce false positives (flagging things that look like secrets but aren't)
- Requires some technical setup

**How to use it (command line):**
```bash
# Scan a GitHub repo directly
trufflehog github --repo https://github.com/your-org/your-repo
```

**Best for:** Teams that want deep, thorough scanning including full Git history.


### 2. GitLeaks
**What it does:**
GitLeaks scans repositories for hardcoded secrets using regex-based rules.
It is lightweight, fast, and highly configurable. It can also be used as a
pre-commit hook, meaning it checks for secrets *before* you even commit them.

**Strengths:**
- Very fast scans
- Highly customizable rules via a config file
- Can be used as a pre-commit hook to prevent secrets from ever being committed
- Clear, readable output reports
- Free and open source

**Weaknesses:**
- Does not scan Git history as deeply as TruffleHog by default
- Rule-based detection may miss unusual or custom secret formats

**How to use it (command line):**
```bash
# Scan a local repo
gitleaks detect --source . --verbose
```

**Best for:** Teams that want fast scanning and want to block secrets
at the commit stage before they ever reach GitHub.


### 3. GitHub Secret Scanning (Built-in)
**What it does:**
GitHub has a built-in secret scanning feature that automatically monitors
your repository for known secret patterns from over 200 service providers.
If a secret is detected, GitHub notifies the repository admin and in some
cases automatically alerts the service provider (e.g., AWS, Stripe) to
revoke the key.

**Strengths:**
- No setup required — works automatically on the repo
- Free for all public repositories
- Available for private repos on GitHub Advanced Security plans
- Automatically alerts service providers to revoke exposed keys
- No command-line knowledge needed

**Weaknesses:**
- Only detects secrets from supported service providers
- Less customizable than TruffleHog or GitLeaks
- Reactive — it catches secrets after they are pushed, not before

**How to enable it:**
1. Go to your repository on GitHub
2. Click **Settings → Code security and analysis**
3. Enable **Secret scanning**

**Best for:** Teams that want a zero-setup safety net with no technical
overhead.


## Recommendation

For our team's current stage of development, a **layered approach** is ideal:

- **Enable GitHub Secret Scanning now** — it requires no setup and provides
  an immediate safety net.
- **Adopt GitLeaks as a pre-commit hook** — this prevents secrets from ever
  being committed in the first place.
- **Run TruffleHog periodically** — to perform deep historical scans and
  ensure nothing was missed in earlier commits.


## What To Do If a Secret Is Found

1. **Revoke the secret immediately** — go to the service (AWS, GitHub, etc.)
   and invalidate the key.
2. **Remove it from the codebase** — move it to an environment variable.
3. **Purge it from Git history** — use `git filter-repo` or contact GitHub
   support.
4. **Audit access logs** — check if the secret was used by anyone unauthorized.