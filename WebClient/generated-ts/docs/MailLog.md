
# MailLog


## Properties

Name | Type
------------ | -------------
`id` | string
`appId` | string
`to` | string
`subject` | string
`body` | string
`isSuccess` | boolean
`errorMessage` | string
`sentAt` | Date

## Example

```typescript
import type { MailLog } from ''

// TODO: Update the object below with actual values
const example = {
  "id": null,
  "appId": null,
  "to": null,
  "subject": null,
  "body": null,
  "isSuccess": null,
  "errorMessage": null,
  "sentAt": null,
} satisfies MailLog

console.log(example)

// Convert the instance to a JSON string
const exampleJSON: string = JSON.stringify(example)
console.log(exampleJSON)

// Parse the JSON string back to an object
const exampleParsed = JSON.parse(exampleJSON) as MailLog
console.log(exampleParsed)
```

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


