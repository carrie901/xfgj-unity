using System;

public class SyncProductCommand : BaseCommand {

    private int sceneId;
    private UpdateDelegate callback;

    public SyncProductCommand (int sceneId, UpdateDelegate callback) {
        this.sceneId = sceneId;
    }

    public override void execute () {
    }

}

