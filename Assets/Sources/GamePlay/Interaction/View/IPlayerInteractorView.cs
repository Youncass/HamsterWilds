namespace Sources.GamePlay.Interaction
{
    public interface IPlayerInteractorView
    {
        void DisplayInteractionHints(InteractionObject interactionObject);
        void RemoveInteractionHints();
    }
}
