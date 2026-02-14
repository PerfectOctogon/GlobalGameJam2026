mergeInto(LibraryManager.library, {
  GetLeaderboardData: function () {
    console.log("[JS] Unity requested leaderboard...");

    function waitForFirebase(callback) {
      if (typeof firebase !== "undefined" && firebase.firestore) {
        callback();
      } else {
        console.log("[JS] Waiting for Firebase...");
        setTimeout(() => waitForFirebase(callback), 200);
      }
    }

    waitForFirebase(async function () {
      console.log("[JS] Firebase ready. Fetching Firestore leaderboard...");

      try {
        const db = firebase.firestore();

        const snapshot = await db
          .collection("leaderboard")
          .orderBy("time")
          .get();

        let players = [];

        snapshot.forEach((doc) => {
          players.push(doc.data());
        });

        console.log("[JS] Players fetched:", players.length);

        const json = JSON.stringify({ players: players });

        if (unityInstance) {
          unityInstance.SendMessage(
            "LeaderboardManager",
            "OnLeaderboardData",
            json
          );
          console.log("[JS] Sent leaderboard data to Unity!");
        } else {
          console.error("[JS] Unity instance not ready.");
        }
      } catch (err) {
        console.error("[JS] Firestore fetch failed:", err);
      }
    });
  }
});
