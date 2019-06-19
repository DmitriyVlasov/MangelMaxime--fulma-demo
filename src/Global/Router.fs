[<RequireQualifiedAccess>]
module Router

open Browser
open Fable.React.Props
open Elmish.Navigation
open Elmish.UrlParser

[<RequireQualifiedAccess>]
type MailboxRoute =
    | Inbox
    | Sent
    | Stared
    | Trash

type Route =
    | Mailbox of MailboxRoute

let private segment (pathA : string) (pathB : string) =
    pathA + "/" + pathB

let private toHash page =
    match page with
    | Mailbox mailboxRoute ->
        match mailboxRoute with
        | MailboxRoute.Inbox ->
            "inbox"
        | MailboxRoute.Sent ->
            "sent"
        | MailboxRoute.Stared ->
            "stared"
        | MailboxRoute.Trash ->
            "trash"
        |> segment "mailbox/"
    |> segment "#/"

let pageParser: Parser<Route -> Route, Route> =
    oneOf
        [
            map (MailboxRoute.Inbox |> Mailbox) (s "mailbox" </> s "inbox")
            map (MailboxRoute.Sent |> Mailbox) (s "mailbox" </> s "sent")
            map (MailboxRoute.Stared |> Mailbox) (s "mailbox" </> s "stared")
            map (MailboxRoute.Trash |> Mailbox) (s "mailbox" </> s "trash")
            map (MailboxRoute.Inbox |> Mailbox) top
        ]

let href route =
    Href (toHash route)

let modifyUrl route =
    route |> toHash |> Navigation.modifyUrl

let newUrl route =
    route |> toHash |> Navigation.newUrl

let modifyLocation route =
    window.location.href <- toHash route
