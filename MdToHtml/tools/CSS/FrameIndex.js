

// Zielelement auswählen
const resizer = document.getElementById("dragMe");
const leftSide = resizer.previousElementSibling;
const rightSide = resizer.nextElementSibling;

// Deklarieren Sie, dass der Wert der Mausposition gespeichert werden soll
let x = 0;
let y = 0;

// Deklarieren Sie, dass bei der Größenänderung das linke Element als Referenz verwendet werden soll
let leftWidth = 0;

// Handler, der ausgeführt wird, wenn im Resizer ein Mausereignis auftritt
const mouseDownHandler = function (e) {
  // Ermitteln Sie den Mauspositionswert und weisen Sie ihn x, y zu
  x = e.clientX;
  y = e.clientY;
  // Fügen Sie den Breitenwert des Ansichtsfensters in das linke Element ein.
  leftWidth = leftSide.getBoundingClientRect().width;

  // Registrieren Sie Mausbewegungen und Release-Ereignisse
  document.addEventListener("mousemove", mouseMoveHandler);
  document.addEventListener("mouseup", mouseUpHandler);
};

const mouseMoveHandler = function (e) {
  // Berechnen Sie beim Bewegen der Maus die Differenz zwischen der vorhandenen anfänglichen Mausposition und dem aktuellen Positionswert.
  const dx = e.clientX - x;
  const dy = e.clientY - y;

  // Mauszeiger während der Größenänderung ändern
  // Bei Anwendung auf class="resizer" wird der Cursor freigegeben, wenn sich die Position ändert, also auf den Körper angewendet
  document.body.style.cursor = "col-resize";

  // Hinzugefügt, um Mausereignisse und Textauswahl in beiden Bereichen (links, rechts) während der Bewegung zu verhindern
  leftSide.style.userSelect = "none";
  leftSide.style.pointerEvents = "none";

  rightSide.style.userSelect = "none";
  rightSide.style.pointerEvents = "none";

  //
  const newLeftWidth =
    ((leftWidth + dx) * 100) / resizer.parentNode.getBoundingClientRect().width;
  leftSide.style.width = `${newLeftWidth}%`;
};

const mouseUpHandler = function () {
  // Alle mit dem Cursor verbundenen Elemente werden entfernt, wenn die Mausbewegung endet
  resizer.style.removeProperty("cursor");
  document.body.style.removeProperty("cursor");

  leftSide.style.removeProperty("user-select");
  leftSide.style.removeProperty("pointer-events");

  rightSide.style.removeProperty("user-select");
  rightSide.style.removeProperty("pointer-events");

  // Registriertes Mausereignis entfernen
  document.removeEventListener("mousemove", mouseMoveHandler);
  document.removeEventListener("mouseup", mouseUpHandler);
};

// Mouse-down-Ereignis registrieren
resizer.addEventListener("mousedown", mouseDownHandler);
