# Production Readiness URL Audit

This document serves as a checklist for the final deployment switch on Monday. The following files contain hardcoded development URLs that must be centralized into environment configurations (e.g., `appsettings.json` or `.env`).

## ces-cms-dashboard
- **`CesCmsDashboard/Pages/Index.cshtml.cs`**
  - `https://www.cesitservice.com` (Currently used as a fallback if config is missing)
  - `https://test.cesrebuild.com/api/seo/faqs` (Currently used as a fallback if config is missing)

## ces-website
- **`js/main.js`**
  - `https://test.cesrebuild.com/api/seo/faqs` (Hardcoded fetch URL)
  - `https://test.cesrebuild.com/api/seo/tech-tips` (Hardcoded fetch URL)

## ces-backend-service
- Expected local `launchSettings.json` and `.http` files contain `localhost` which are safe and do not need to be modified for production deployment.

*(Note: All instances of `http://www.w3.org/2000/svg` found in HTML files were ignored as they are standard XML namespaces).*
