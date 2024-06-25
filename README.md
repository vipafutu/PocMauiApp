# PocMauiApp

PocMauiApp is a basic app written in .NET MAUI, inspired by a Udemy course but with substantial modifications. This project adopts the MVVM (Model-View-ViewModel) pattern, which was not implemented in the original course. Please note that the backend is not included in this .NET MAUI project.

## Udemy Course

This project is based on the Udemy course: [Build Real World ECommerce App with .NET MAUI](https://www.udemy.com/course/build-real-world-ecommerce-app-with-net-maui/). The original code has been heavily modified.

The course has some limitations, including poorly written C# code and a lack of the MVVM (Model-View-ViewModel) pattern, which is a important feature of the XAML development. Dependency Injection was not used at all etc..
Therefore, I do not recommend this course for those looking to learn best practices in .NET MAUI development.

## Features Tested

- Dependency Injections
- Push Notifications
  - Implemented using the OneSignal service.
- Map Integration
- Basic XAML Pages with MVVM Pattern
- Location Services and Map Integration

## Getting Started

## iOS

Info.plist file in ./Platforms/iOS/ -directory should modified

    <dict>
        <key>NSLocationWhenInUseUsageDescription</key>
        <string>We need your location to provide better services and features.</string>
        ...
    </dict>

> dotnet build -t:Run -f net8.0-ios


## Android

google-services.json should be placed into ./Platforms/Android/ -directory and must contain keys for map service.

For example:

    {
        "project_info": {
            "project_number": "970210267115",
            "project_id": "pocmauiapp",
            "storage_bucket": "pocmauiapp.appspot.com"
        },
        "client": [
            {
            "client_info": {
                "mobilesdk_app_id": "xxx",
                "android_client_info": {
                "package_name": "com.palski.pocmauiapp"
                }
            },
            "oauth_client": [],
            "api_key": [
                {
                "current_key": "xxx"
                }
            ],
            "services": {
                "appinvite_service": {
                "other_platform_oauth_client": []
                }
            }
            }
        ],
        "configuration_version": "1"
    }

> dotnet build -t:Run -f net8.0-android


