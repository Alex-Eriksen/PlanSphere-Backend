namespace PlanSphere.Core.Constants;

public static class EmailTemplates
{
    public const string ResetPassword = """
                                        <!DOCTYPE html>
                                        <html lang="da">
                                        <head>
                                          <meta charset="UTF-8">
                                          <meta name="viewport" content="width=device-width, initial-scale=1.0">
                                          <title>Nulstil din adgangskode</title>
                                          <style>
                                            body {{
                                              font-family: Arial, sans-serif;
                                              color: #333;
                                              line-height: 1.6;
                                            }}
                                            .container {{
                                              max-width: 600px;
                                              margin: 0 auto;
                                              padding: 20px;
                                              border: 1px solid #ddd;
                                              border-radius: 8px;
                                            }}
                                            .header {{
                                              background-color: #339989;
                                              color: #ffffff;
                                              padding: 20px;
                                              text-align: center;
                                              font-size: 24px;
                                              font-weight: bold;
                                            }}
                                            .content {{
                                              padding: 20px;
                                              font-size: 16px;
                                            }}
                                            .button {{
                                              display: inline-block;
                                              margin-top: 20px;
                                              padding: 15px 30px;
                                              background-color: #339989;
                                              color: #ffffff;
                                              text-decoration: none;
                                              border-radius: 5px;
                                              font-size: 16px;
                                              font-weight: bold;
                                              text-align: center;
                                            }}
                                            .footer {{
                                              margin-top: 20px;
                                              font-size: 12px;
                                              color: #777;
                                              text-align: center;
                                            }}
                                          </style>
                                        </head>
                                        <body>
                                          <div class="container">
                                            <div class="header">
                                              Nulstil din adgangskode
                                            </div>
                                            <div class="content">
                                              <p>Kære {0},</p>
                                              <p>Vi har modtaget en anmodning om at nulstille din adgangskode til din konto på Plansphere.</p>
                                              <p>Hvis du ikke har anmodet om nulstilling, kan du se bort fra denne email. Hvis du ønsker at fortsætte med at nulstille din adgangskode, klik venligst på knappen nedenfor:</p>
                                              <a href="{1}" class="button">Nulstil Adgangskode</a>
                                              <p>Bemærk: Linket til nulstilling af adgangskoden udløber efter 24 timer af sikkerhedshensyn.</p>
                                            </div>
                                            <div class="footer">
                                              <p>Har du spørgsmål? Kontakt os på support@plansphere.com</p>
                                              <p>Med venlig hilsen,<br>Plansphere Teamet</p>
                                            </div>
                                          </div>
                                        </body>
                                        </html>
                                        """;
  
    public const string Invitation = """
                                     <!DOCTYPE html>
                                     <html lang="da">
                                     <head>
                                       <meta charset="UTF-8">
                                       <meta name="viewport" content="width=device-width, initial-scale=1.0">
                                       <title>Invitation til Plansphere</title>
                                       <style>
                                         body {{
                                           font-family: Arial, sans-serif;
                                           color: #333;
                                           line-height: 1.6;
                                         }}
                                         .container {{
                                           max-width: 600px;
                                           margin: 0 auto;
                                           padding: 20px;
                                           border: 1px solid #ddd;
                                           border-radius: 8px;
                                         }}
                                         .header {{
                                           background-color: #339989;
                                           color: #ffffff;
                                           padding: 20px;
                                           text-align: center;
                                           font-size: 24px;
                                           font-weight: bold;
                                         }}
                                         .content {{
                                           padding: 20px;
                                           font-size: 16px;
                                         }}
                                         .button {{
                                           display: inline-block;
                                           margin-top: 20px;
                                           padding: 15px 30px;
                                           background-color: #339989;
                                           color: #ffffff;
                                           text-decoration: none;
                                           border-radius: 5px;
                                           font-size: 16px;
                                           font-weight: bold;
                                           text-align: center;
                                         }}
                                         .footer {{
                                           margin-top: 20px;
                                           font-size: 12px;
                                           color: #777;
                                           text-align: center;
                                         }}
                                       </style>
                                     </head>
                                     <body>
                                       <div class="container">
                                         <div class="header">
                                           Invitation til Plansphere
                                         </div>
                                         <div class="content">
                                           <p>Kære {0},</p>
                                           <p>Vi er glade for at invitere dig til at deltage i Plansphere – et program, der hjælper dig med at holde styr på din arbejdstid.</p>
                                           <p>Klik på knappen nedenfor for at komme i gang:</p>
                                           <a href="{1}" class="button">Deltag i Plansphere</a>
                                         </div>
                                         <div class="footer">
                                           <p>Har du spørgsmål? Kontakt os venligst på support@plansphere.com</p>
                                           <p>Med venlig hilsen,<br>Plansphere Teamet</p>
                                         </div>
                                       </div>
                                     </body>
                                     </html>
                                     """;
}