//
//  IosPlugin.m
//  Unity-iPhone
//
//  Created by 周 龙 on 13-12-16.
//
//
#import "IosPlugin.h"
#import "RecommendViewController.h"
#import "UnityAppController.h"

@implementation IosPlugin


@end

// Converts C style string to NSString
NSString* CreateNSString (const char* string) {
	if (string)
		return [NSString stringWithUTF8String: string];
	else
		return [NSString stringWithUTF8String: ""];
}

// Helper method to create C string copy
char* MakeStringCopy (const char* string) {
	if (string == NULL)
		return NULL;
	char* res = (char*)malloc(strlen(string) + 1);
	strcpy(res, string);
	return res;
}

extern "C" {
    
	char* GetBundleVersion() {
        NSString* version = [[[NSBundle mainBundle] infoDictionary] objectForKey:(NSString *)kCFBundleVersionKey];
        return MakeStringCopy([version UTF8String]);
    }
    
    void OpenWebsite(const char* url) {
        NSLog(@"%@", CreateNSString(url));
        RecommendViewController *viewController = [[RecommendViewController alloc] initWithNibName:@"RecommendViewController" bundle:nil];
        viewController.url = CreateNSString(url);
        [[UIApplication sharedApplication].keyWindow.rootViewController presentViewController:viewController animated:YES completion:nil];
    }
    
    long GetAvailableSpace() {
        return 1l;
    }
	
}
