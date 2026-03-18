# US Data Privacy Laws — Developer Checklist
## Iteration 3

Checklist for our team to make sure we are complying with United States 
privacy laws in website development


## The Laws We Need To Know

### COPPA — Children's Online Privacy Protection Act
Applies to any website that knowingly collects data from children under 13.

- [ ] If your site could be used by children under 13, consult legal counsel
      before collecting any data from them
- [ ] Do not collect personal information from users under 13 without
      verifiable parental consent
- [ ] Provide a clear privacy policy that explains what data is collected
      and how it is used
- [ ] Give parents the ability to review and delete their child's data
- [ ] Do not require more information than necessary for a child to
      participate in an activity

**Key rule:** While our website is not aimed at minors, it might be relevant to
add an age gate to prevent unintended colleciton of minor data


### CCPA — California Consumer Privacy Act
Applies to for-profit businesses that collect data from California residents
and meet at least one of the following: annual gross revenue over $25 million,
buy/sell data of 100,000+ consumers per year, or earn 50%+ of revenue
from selling consumer data.

- [ ] Publish a clear and accessible Privacy Policy on your site
- [ ] Inform users what categories of personal data you collect and why
- [ ] Provide a "Do Not Sell or Share My Personal Information" option
      if you share data with third parties
- [ ] Honor user requests to access, delete, or correct their personal data
      within 45 days
- [ ] Do not discriminate against users who exercise their privacy rights
      (e.g., don't deny service to users who opt out)
- [ ] Update your Privacy Policy at least once every 12 months

**Key rule:** Relevant to any users who interact while living in California


### HIPAA — Health Insurance Portability and Accountability Act
Applies to any site or application that handles Protected Health Information
(PHI), such as medical records, health history, or insurance data.

- [ ] Identify whether your application handles any Protected Health
      Information (PHI)
- [ ] If yes, ensure all PHI is encrypted in transit (HTTPS) and at rest
- [ ] Restrict access to PHI to only team members who need it
- [ ] Log all access to PHI for auditing purposes
- [ ] Have a signed Business Associate Agreement (BAA) with any third-party
      services that handle PHI (e.g., cloud storage, email providers)
- [ ] Create and document a breach notification procedure

**Key rule:** HIPPA does not apply to our site unless we drastically change the concept


### FERPA — Family Educational Rights and Privacy Act
Applies to any application used by educational institutions that handles
student education records.

- [ ] Identify whether your application stores or processes student
      education records
- [ ] If yes, ensure student records are only accessible to authorized
      school officials and the students themselves
- [ ] Do not share student records with third parties without written
      consent from the student (or parent if under 18)
- [ ] Provide students (or parents) the ability to review and request
      corrections to their records
- [ ] Document your data retention and deletion policy for student records

**Key rule:** Relevant if we decide to take in and store any
student data


### General Best Practices (Apply to All Laws)

- [ ] Write a Privacy Policy and link it clearly in the site footer
- [ ] Only collect data you actually need (data minimization)
- [ ] Tell users exactly what data you are collecting and why
- [ ] Allow users to request deletion of their data
- [ ] Use HTTPS everywhere to protect data in transit
- [ ] Store as little sensitive data as possible
- [ ] Regularly review what data your application is collecting
      and delete what is no longer needed
- [ ] When in doubt, consult a legal professional — this checklist
      is a starting point, not legal advice


## Quick Reference — Which Laws Apply to Us?

| Law    | Applies If...                                      | Most Relevant Action         |
|--------|----------------------------------------------------|------------------------------|
| COPPA  | Site may be accessed by users under 13             | Add age gate, get parental consent |
| CCPA   | You have California users + meet revenue thresholds| Add privacy policy + opt-out option |
| FERPA  | App stores student educational records             | Restrict access, allow review |

---

*Note: This document is for educational purposes and does not constitute legal advice.*