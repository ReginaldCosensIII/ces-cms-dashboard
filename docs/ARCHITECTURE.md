# System Architecture

## 1. Overview: The Decoupled "Headless" Pattern
The CES CMS and website utilize a decoupled, headless architecture. This pattern separates the content management and database routing (the "head") from the public-facing presentation layer. 

This approach ensures:
* **Maximum Security:** The public web server has no direct connection string or access to the PostgreSQL database.
* **High Performance:** The public site remains static, requiring zero server-side rendering for standard page loads.
* **Separation of Concerns:** CMS UI updates do not risk breaking public-facing routing or design.

## 2. Core System Components

### A. The CMS Dashboard (This Repository)
- **Role:** Secure internal portal for CES staff to author, edit, and publish content.
- **Technology:** ASP.NET Core Razor Pages.
- **UI Architecture:** Standardized on Bootstrap 5. Utilizes a custom CSS variable system (`site.css`) mapped to CES brand guidelines (e.g., `--color-dark-slate`, no pure absolute blacks).
  
  #### Dynamic UI & Modal Architecture
  When editing entities within a data table loop, the application must use the AJAX Partial View pattern rather than rendering hidden modals in a foreach loop. This prevents the jQuery Unobtrusive Validation "Duplicate DOM Name Trap", avoids Quill.js initialization race conditions, and ensures clean ModelState validation.
  
  Post handlers for multiple forms on a single page must use `ModelState.Clear()` followed by explicit `TryValidateModel(TargetObject)` to prevent cross-contamination of implicit required properties (like Guids).

- **Rich Text Management:** Integrates `Quill.js`. The editor is strictly locked down via JavaScript initialization to prevent users from applying custom fonts, colors, or headings that violate brand standards. Allowed inputs are limited to bold, italic, underline, strike, lists, and links.

### B. The Data API Layer (This Repository)
- **Role:** A lightweight set of ASP.NET Core Controllers designed strictly to serve JSON data.
- **Behavior:** Queries the PostgreSQL database via Entity Framework Core, applies any necessary caching logic, and exposes endpoints (e.g., `GET /api/public/faqs`) containing only published content.

### C. The Public Frontend (`ces-website` Repository)
- **Role:** The public-facing website consumed by end-users.
- **Technology:** Static HTML, CSS, and Vanilla JavaScript.
- **Behavior:** On page load, static pages (like `faq.html`) execute a JavaScript `fetch()` request to the Data API Layer. The returned JSON is dynamically parsed and injected into the DOM.
- **SEO Architecture:** Technical SEO (Rich Snippets) is handled client-side. The frontend JavaScript dynamically generates `FAQPage` or `Article`/`HowTo` JSON-LD schema based on the fetched API data and injects it into the `<head>` of the document for Googlebot to crawl.

## 3. Hosting & Deployment Strategy
- The application is designed to be hosted on Windows IIS.
- The CMS Dashboard and API are configured to run as an IIS Sub-Application (e.g., under the `/api` or `/cms` virtual directory) beneath the primary CES domain or a designated internal sub-domain.
- Cross-Origin Resource Sharing (CORS) policies must be explicitly defined in `Program.cs` to allow the public static site domain to consume the API endpoints securely.