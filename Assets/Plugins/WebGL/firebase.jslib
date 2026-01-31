mergeInto(LibraryManager.library, {
  getLeaderboardData: function () {

    console.log("Unity requested leaderboard...");

    function waitForFirebase(callback) {
      if (typeof firebase !== "undefined") {
        callback();
      } else {
        console.log("Waiting for Firebase...");
        setTimeout(() => waitForFirebase(callback), 200);
      }
    }

    waitForFirebase(async function () {

      console.log("Firebase ready, fetching Firestore...");

      const db = firebase.firestore();

      try {
        const snapshot = await db.collection("leaderboard")
          .orderBy("time")
          .limit(10)
          .get();

        let players = [];
        snapshot.forEach(doc => {
          players.push(doc.data());
        });

        const json = JSON.stringify({ players: players });

        if (unityInstance) {
          unityInstance.SendMessage(
            "LeaderboardImage",
            "OnLeaderboardData",
            json
          );
          console.log("Leaderboard sent to Unity.");
        } else {
          console.error("Unity instance not ready.");
        }

      } catch (err) {
        console.error("Firestore fetch failed:", err);
      }
    });
  }
});
