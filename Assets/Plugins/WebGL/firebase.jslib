mergeInto(LibraryManager.library, {
  getLeaderboardData: function () {

    console.log("Unity requested leaderboard...");

    const db = firebase.firestore();

    db.collection("leaderboard")
      .orderBy("time")
      .limit(10)
      .get()
      .then(snapshot => {

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
      })
      .catch(err => {
        console.error("Firestore fetch failed:", err);
      });
  },

  submitScore: function (namePtr, timeValue) {

    const playerName = UTF8ToString(namePtr);

    console.log("Submitting score:", playerName, timeValue);

    const db = firebase.firestore();

    db.collection("leaderboard").add({
      name: playerName,
      time: timeValue
    })
    .then(() => {
      console.log("Score uploaded successfully!");
    })
    .catch(err => {
      console.error("Upload failed:", err);
    });
  }
});
