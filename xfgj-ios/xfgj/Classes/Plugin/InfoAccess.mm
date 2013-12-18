//
//  InfoAccess.m
//  Unity-iPhone
//
//  Created by 周 龙 on 13-12-16.
//
//
#import "InfoAccess.h"

@implementation InfoAccess


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
	
}
