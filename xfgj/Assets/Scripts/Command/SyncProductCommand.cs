using System;

public class SyncProductCommand : BaseCommand {

    private int sceneId;
    private UIDelegate.Update callback;

    public SyncProductCommand (int sceneId, UIDelegate.Update callback) {
        this.sceneId = sceneId;
    }

    public override void execute () {
    }

}

