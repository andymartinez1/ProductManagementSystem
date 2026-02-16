Add this to appsettings.json to enable the email service:

```json
{
  "Email": {
    "From": {
      "Address": "tremayne.kunze@ethereal.email",
      "Name": "Product Management System"
    },
    "Smtp": {
      "Host": "smtp.ethereal.email",
      "Port": "587",
      "Username": "tremayne.kunze@ethereal.email",
      "Password": "wGKmsApFwbc6hJtZW1"
    }
  }
}
```

In [Ethereal Email](https://ethereal.email/) (A fake SMTP email service), login using the following credentials:
Username: tremayne.kunze@ethereal.email
Password: wGKmsApFwbc6hJtZW1
![img.png](img.png)
Go to messages:
![img_1.png](img_1.png)

Confirmation, password reset and forgotten password emails will be sent here.